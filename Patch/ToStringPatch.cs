using Il2CppGameLogic;
using HarmonyLib;
using System.Text;

namespace BestCombinationSuggest.Patch {
    //[HarmonyPatch(typeof(MusicData), nameof(MusicData.ToString))]
    public class ToStringPatch {
        public static bool Prefix(ref string __result, MusicData __instance) {
            StringBuilder sb = new StringBuilder(30);
            sb.Append((double) (__instance.configData.time)).Append("t ");
            switch (__instance.noteData.type) {
                case 1:
                    sb.Append("普通");
                    break;
                case 2:
                    sb.Append("齿轮");
                    break;
                case 3:
                    sb.Append("长按");
                    if (__instance.isLongPressStart) {
                        sb.Append("头");
                    }
                    if (__instance.isLongPressEnd) {
                        sb.Append("尾");
                    }
                    break;
                case 4:
                    sb.Append("幽灵");
                    break;
                case 5:
                    sb.Append("近身");
                    break;
                case 6:
                    sb.Append("爱心");
                    break;
                case 7:
                    sb.Append("音符");
                    break;
                case 8:
                    sb.Append("连击");
                    sb.Append(__instance.GetMulHitHighThreshold())
                        .Append(" len:").Append((double)__instance.configData.length);
                    break;
                default:
                    sb.Append("type");
                    sb.Append(__instance.noteData.type);
                    break;
            }
            if (__instance.isBossNote) {
                sb.Append(" boss");
            }
            if (__instance.isDouble) {
                sb.Append(" 双押");
            }
            if (__instance.configData.blood) {
                sb.Append(" 爱心");
            }
            if (__instance.noteData.addCombo) {
                sb.Append(" combo");
            }
            if (__instance.IsBossNearAttack) {
                sb.Append(" bossatk");
            }
            sb.Append(" fever").Append(__instance.noteData.fever)
                .Append(" score").Append(__instance.noteData.score);
            __result = sb.ToString();
            return false;
        }
    }
}
