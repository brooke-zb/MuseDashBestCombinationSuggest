using Il2CppAssets.Scripts.GameCore.HostComponent;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppAssets.Scripts.PeroTools.Managers;
using Il2CppGameLogic;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BestCombinationSuggest.Rules {
    // 计分组合
    public class Combination {
        public string Name {
            get {
                string roleName = Singleton<ConfigManager>.instance.GetConfigStringValue("character", roleRule.GetID(), "cosName") ?? "";
                string elfinName = Singleton<ConfigManager>.instance.GetConfigStringValue("elfin", elfinRule.GetID(), "name") ?? "";
                return roleName + " / " + elfinName;
            }
        }
        private IRule roleRule;
        private IRule elfinRule;

        public Combination(IRule roleRule, IRule elfinRule) {
            this.roleRule = roleRule;
            this.elfinRule = elfinRule;
        }

        public SimulationResult ApplyRule(List<MusicData> chart) {
            SimulationResult result = new();
            result.CombName = this.Name;

            // 若为小恶魔组合则先计算能否通关
            if (roleRule.GetID() == RoleID.MARIJA_DEVIL) {
                if (!CanDevilSurvive(chart, false, 98)) {
                    bool canSurvive = false;
                    foreach (int drainTick in drainTicks) {
                        if (CanDevilSurvive(chart, true, drainTick)) {
                            canSurvive = true;
                            result.IsStable = false;
                            break;
                        }
                    }
                    if (!canSurvive) {
                        return result;
                    }
                }
            }

            // 采用基础型分值的常规note：最终P得分=(分值*连击倍率)*(1+Fever加成+角色加成+精灵加成)+绑定红心分值*(1+角色加成)
            // 采用奖励型分值的常规note：最终P得分=分值*(1+角色加成)
            int combo = 0;
            int feverEnergy = 0;
            double feverStartTick = -114514;
            int totalScore = 0;
            bool hasDouble = false;
            foreach (var note in chart) {
                // 若一组双押已计算其中一个，则另一个跳过
                if (note.isDouble) {
                    if (hasDouble) {
                        hasDouble = false;
                        continue;
                    } else {
                        hasDouble = true;
                    }
                }
                double curTick = (double) note.tick;
                bool isInFever = curTick >= feverStartTick && curTick <= feverStartTick + 5.0d;

                // 计算combo
                if (note.noteData.addCombo && !note.isLongPressing) {
                    combo++;
                    if (note.isDouble) combo++;
                }

                // 计算fever
                if (!isInFever && !note.isLongPressing) {
                    feverEnergy += note.noteData.fever;
                }
                if (feverEnergy >= ModConstants.MAX_FEVER_ENERGY) {
                    feverStartTick = curTick;
                    // 连击note取结束时间为fever起点
                    if (note.isMul) {
                        feverStartTick = (double) note.configData.length;
                    }
                    feverEnergy = 0;
                    isInFever = true;
                }

                // 计算基础型分值加成
                if (note.noteData.type == NoteType.Monster || note.noteData.type == NoteType.Hide || (note.isLongPressType && !note.isLongPressing) || note.noteData.type == NoteType.Boss) {
                    // combo倍率
                    float comboRatio = 1f + roleRule.GetComboRatio(combo);

                    // 基础/fever加成倍率
                    float skillAndFeverRatio = 1f + roleRule.GetBaseRatio(note) + elfinRule.GetBaseRatio(note);
                    if (isInFever) {
                        skillAndFeverRatio += 0.5f + roleRule.GetFeverRatio(note) + elfinRule.GetFeverRatio(note);
                    }
                    int score = Mathf.CeilToInt(note.noteData.score * comboRatio * skillAndFeverRatio);

                    // 计算附加在note上的HP道具分值
                    if (note.configData.blood) {
                        score += Mathf.RoundToInt(ModConstants.HEART_SCORE * (1f + roleRule.GetBounsRatio(note)));
                    }

                    totalScore += score;
                }
                // 计算奖励型分值加成
                else if (note.noteData.type == NoteType.Music || note.noteData.type == NoteType.Hp || note.noteData.type == NoteType.Block) {
                    // 技能加成
                    float skillRatio = 1f + roleRule.GetBounsRatio(note);

                    totalScore += Mathf.RoundToInt(note.noteData.score * skillRatio);
                }
                else if (note.isLongPressType && note.isLongPressing) {
                    totalScore += ModConstants.PRESSING_SCORE;
                }
                else if (note.isMul) {
                        totalScore += note.noteData.score + note.GetMulHitHighThreshold() * ModConstants.MULTI_HIT_SCORE;
                }
                // MelonLoader.Melon<MainMod>.Logger.Msg(combo + "cb " + totalScore + " " + note.ToString());
            }
            result.score = totalScore;
            return result;
        }

        private static readonly int[] drainTicks = { 25, 75, 98 };
        private bool CanDevilSurvive(List<MusicData> chart, bool tryEarlyHit, int drainTick) {
            int hp = ModConstants.DEVIL_MAX_HP;
            int pressingCount = 0;
            int prevTick = -1;
            bool hasDouble = false;
            foreach (var note in chart) {
                // 若一组双押已计算其中一个，则另一个跳过
                if (note.isDouble) {
                    if (hasDouble) {
                        hasDouble = false;
                        continue;
                    } else {
                        hasDouble = true;
                    }
                }

                // debug
                // StringBuilder sb = new StringBuilder();

                int curTick = (int) (note.tick * 1000);
                // debug
                // sb.Append("tick: ").Append(curTick);

                if (prevTick == -1) {
                    prevTick = curTick - (curTick % 100);
                }

                // 计算失血值
                // 小恶魔失血是在整100ms执行的
                int duration = curTick - prevTick;
                if (duration > 0 && pressingCount <= 0) {
                    int durationToDrain = (drainTick + 100 - (prevTick % 100)) % 100; // 计算下一个失血tick需要多久
                    int durationFromDrain = duration - durationToDrain;
                    int drain = (durationFromDrain + 100) / 100; // 加上100解决抹除小数问题

                    // 尝试early perfect减少失血
                    if (tryEarlyHit) {
                        if (note.isLongPressStart && (durationFromDrain - ModConstants.EARLY_NORMAL_THRESHOLD) / 100 < durationFromDrain / 100) {
                            drain--;
                        }
                        else if (note.isMul && note.GetMulHitHighThreshold() > 1 && (durationFromDrain - ModConstants.EARLY_MAX_THRESHOLD) / 100 < durationFromDrain / 100) {
                            drain--;
                        }
                        else if (note.noteData.type != NoteType.Block && (durationFromDrain - ModConstants.EARLY_NORMAL_THRESHOLD) / 100 < durationFromDrain / 100) {
                            drain--;
                            curTick -= ModConstants.EARLY_NORMAL_THRESHOLD;
                        }
                    }

                    hp -= drain;
                    // debug
                    // sb.Append(" loss: ").Append(drain);
                }

                if (hp <= 0) {
                    // debug
                    // MelonLogger.Msg(sb);
                    // MelonLogger.Msg($"drainTick为{drainTick}{(tryEarlyHit ? "并提前按长条" : "")}时, 第{curTick / 1000.0d}秒前寄了");
                    return false;
                }

                // 计算回血
                int debugHp = hp;
                if (NoteType.isLilithRecoveryNote(note)) {
                    if (note.noteData.type == NoteType.Hp) {
                        hp += ModConstants.HP_RECOVERY;
                    }
                    else {
                        hp += note.isDouble ? 2 * ModConstants.LILITH_RECOVERY : ModConstants.LILITH_RECOVERY;
                        if (note.configData.blood) {
                            hp += ModConstants.HP_RECOVERY;
                        }
                    }
                    if (hp > ModConstants.DEVIL_MAX_HP) {
                        hp = ModConstants.DEVIL_MAX_HP;
                    }
                    // debug
                    // sb.Append(" recover: ").Append(hp - debugHp);
                }
                // debug
                // sb.Append(" hp: ").Append(hp);

                // 计算长按
                if (note.isLongPressStart) {
                    pressingCount++;
                    // debug
                    // sb.Append(" hold start");
                }
                else if (note.isLongPressEnd) {
                    pressingCount--;
                    // debug
                    // sb.Append(" hold end");
                }

                // 计算连击
                if (note.isMul) {
                    // debug
                    // sb.Append(" mul");
                    prevTick = curTick + (int) (note.configData.length * 1000);
                } else {
                    prevTick = curTick;
                }
                // MelonLogger.Msg(sb);
            }
            // MelonLogger.Msg($"drainTick为{drainTick}{(tryEarlyHit ? "并提前按长条" : "")}时，最后剩下{hp}点HP");
            return true;
        }
    }
}
