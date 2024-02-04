using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppFormulaBase;
using Il2CppGameLogic;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using Task = Il2CppSystem.Threading.Tasks.Task;
using Action = Il2CppSystem.Action;

namespace BestCombinationSuggest {
    public class BestCombinationMelon : MelonMod {
        private static Font font = null;
        private static GameObject canvas = null;
        private static GameObject msgStable = null;
        private static GameObject msgInstable = null;
        private static BestCombination bestCombination = null;

        private bool hasCalculate = true;

        public override void OnLateInitializeMelon() {
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            if (sceneName == "GameMain") {
                hasCalculate = false;
                if (font == null) {
                    AsyncOperationHandle<Font> handle = Addressables.LoadAssetAsync<Font>(ModConstants.FONT_NAME);
                    font = handle.WaitForCompletion();
                }
                setCanvas();
            }
        }

        public override void OnUpdate() {
            // 进入游戏时计算最高得分组合
            if (!hasCalculate) {
                var chart = new List<MusicData>();
                foreach (var note in Singleton<StageBattleComponent>.instance.GetMusicData()) {
                    // 只储存note
                    if (NoteType.isNote(note)) {
                        chart.Add(note);
                    }
                }
                bestCombination = null;
                Calculate(chart);
                hasCalculate = true;
            }
        }

        public override void OnDeinitializeMelon() {
            Addressables.Release(font);
        }

        private void Calculate(List<MusicData> chart) {
            // 计算量大，放到线程池内运行
            Task.Run((Action) (() => ScoreCalculator.CalculateChart(chart, result => bestCombination = result)));
        }

        private static void setCanvas() {
            canvas = new(ModConstants.CANVAS_NAME);
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Camera_2D").GetComponent<Camera>();
            canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920f, 1080f);
            canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvas.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        }

        public static void Show() {
            string textStable = null;
            string textInstable = null;
            if (bestCombination != null) {
                var best = bestCombination.GetBestComb();
                if (best.IsStable) {
                    textStable = best.CombName;
                } else {
                    var bestStable = bestCombination.GetBestStableComb();
                    textStable = bestStable.CombName;
                    textInstable = best.CombName;
                }
            }
            if (textInstable != null) {
                msgInstable = new(ModConstants.OBJ_NAME_INSTABLE);
                msgInstable.transform.SetParent(canvas.transform);
                Text textCompInstable = msgInstable.AddComponent<Text>();
                textCompInstable.text = textInstable;
                textCompInstable.alignment = TextAnchor.LowerCenter;
                textCompInstable.fontSize = 40;
                textCompInstable.color = Color.red;
                textCompInstable.font = font;
                textCompInstable.transform.localPosition = new Vector3(0f, -180f, 0f);
                RectTransform rectInstable = textCompInstable.GetComponent<RectTransform>();
                rectInstable.sizeDelta = new Vector2(600f, 60f);
                rectInstable.localScale = new Vector3(1f, 1f, 1f);
            }
            msgStable = new(ModConstants.OBJ_NAME_STABLE);
            msgStable.transform.SetParent(canvas.transform);
            Text textCompStable = msgStable.AddComponent<Text>();
            textCompStable.text = textStable != null ? textStable : ModConstants.RESULT_PLACEHOLDER;
            textCompStable.alignment = TextAnchor.LowerCenter;
            textCompStable.fontSize = 40;
            textCompStable.color = textStable != null ? Color.yellow : Color.white;
            textCompStable.font = font;
            if (textInstable != null) {
                textCompStable.transform.localPosition = new Vector3(0f, -230f, 0f);
            } else {
                textCompStable.transform.localPosition = new Vector3(0f, -180f, 0f);
            }
            RectTransform rectStable = textCompStable.GetComponent<RectTransform>();
            rectStable.sizeDelta = new Vector2(600f, 60f);
            rectStable.localScale = new Vector3(1f, 1f, 1f);
        }

        public static void Hide() {
            if (msgStable != null) {
                UnityEngine.Object.Destroy(msgStable);
                msgStable = null;
            }
            if (msgInstable != null) {
                UnityEngine.Object.Destroy(msgInstable);
                msgInstable = null;
            }
        }
    }
}
