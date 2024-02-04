using Il2CppAssets.Scripts.Database;
using HarmonyLib;
using Il2Cpp;

namespace BestCombinationSuggest.Patch {
    // 暂停时显示最高得分组合
    [HarmonyPatch(typeof(GameMainPnlPause), nameof(GameMainPnlPause.Show))]
    public class PausePatch {
        public static void Postfix() {
            BestCombinationMelon.Show();
        }
    }

    // 非暂停时隐藏最高得分组合
    [HarmonyPatch(typeof(GameMainPnlPause), nameof(GameMainPnlPause.OnYesClicked))]
    public class ResumePatch {
        public static void Postfix() {
            BestCombinationMelon.Hide();
        }
    }
    [HarmonyPatch(typeof(BattleHelper), nameof(BattleHelper.GameFinish))]
    public class ExitPatch {
        public static void Postfix() {
            BestCombinationMelon.Hide();
        }
    }
    [HarmonyPatch(typeof(BattleHelper), nameof(BattleHelper.GameRestart))]
    public class RestartPatch {
        public static void Postfix() {
            BestCombinationMelon.Hide();
        }
    }
}
