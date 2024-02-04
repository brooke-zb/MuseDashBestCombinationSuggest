using BestCombinationSuggest.Rules;
using BestCombinationSuggest.Rules.Role;
using BestCombinationSuggest.Rules.Elfin;
using Il2CppGameLogic;
using System;
using System.Collections.Generic;

namespace BestCombinationSuggest {
    public class ScoreCalculator {
        private static List<Combination> combinations = new List<Combination>();

        static ScoreCalculator() {
            // 添加计分组合
            // combinations.Add(new Combination(SleepyRin.instance, NoneElfin.instance));
            combinations.Add(new Combination(BunnyRin.instance, LittleWitch.instance));
            combinations.Add(new Combination(BunnyRin.instance, DragonGirl.instance));
            combinations.Add(new Combination(BunnyRin.instance, Lilith.instance));
            combinations.Add(new Combination(JokerBuro.instance, LittleWitch.instance));
            combinations.Add(new Combination(JokerBuro.instance, DragonGirl.instance));
            combinations.Add(new Combination(JokerBuro.instance, Lilith.instance));
            combinations.Add(new Combination(DevilMarija.instance, Lilith.instance));
        }

        public static void CalculateChart(List<MusicData> chart, Action<BestCombination> callback) {
            chart.Sort(CompareMusicData);
            BestCombination bestCombination = new();
            foreach (var combination in combinations) {
                // Melon<MaxScoreMelon>.Logger.Msg(combination.Name + " result score: " + score);
                // 妖怪録、我し来にけり。  425910
                // notice               226280
                bestCombination.AddResult(combination.ApplyRule(chart));
            }

            callback(bestCombination);
        }

        private static int CompareMusicData(MusicData x, MusicData y) {
            // 优先按tick排序
            if (x.tick != y.tick) {
                return x.tick > y.tick ? 1 : -1;
            }
            // 其次按time排序
            if (x.configData.time != y.configData.time) {
                return x.configData.time > y.configData.time ? 1 : -1;
            }
            // 时间一致则按score排序
            if (x.noteData.score != y.noteData.score) {
                return x.noteData.score > y.noteData.score ? 1 : -1;
            }
            return 0;
        }
    }
}
