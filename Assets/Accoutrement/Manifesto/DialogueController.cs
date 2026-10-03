using Ink.Runtime;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    public class DialogueController : MonoBehaviour
    {
        public static DialogueController Instance { get; private set; }

        public TextAsset inkJsonAsset;
        private Story story;

        private void Awake()
        {
            Instance = this;
            story = new Story(inkJsonAsset.text);
        }

        // 캐릭터나 오브젝트 버튼에서 호출
        public void TriggerInteraction(string buttonActionId)
        {
            var currentScreen = ScreenManager.Instance.CurrentScreenView;
            if (currentScreen == null) return;

            // 예: "Scene_1" + "." + "btn_c1" -> "Scene_1.btn_c1"
            string fullPath = $"{currentScreen.sceneKnotName}.{buttonActionId}";
        
            story.ChoosePathString(fullPath);
            PlayDialogueBlock();
        }

        private void PlayDialogueBlock()
        {
            var currentScreen = ScreenManager.Instance.CurrentScreenView;

            while (story.canContinue)
            {
                string rawText = story.Continue().Trim();

                // 1. 화자 및 본문 분리 ("c1: 대사 내용")
                string speaker = "";
                string message = rawText;

                if (rawText.Contains(":"))
                {
                    string[] parts = rawText.Split(new char[] { ':' }, 2);
                    speaker = parts[0].Trim();
                    message = parts[1].Trim().Trim('"'); // 따옴표 제거
                }

                // 2. UI 텍스트 반영
                if (currentScreen.speakerNameText != null) 
                    currentScreen.speakerNameText.text = speaker;
                if (currentScreen.dialogueText != null) 
                    currentScreen.dialogueText.text = message;

                // 3. 발화자 하이라이트 자동화 (말하는 캐릭터만 흰색, 나머진 딤)
                currentScreen.UpdateCharacterFocus(speaker);

                // 4. 태그 처리 (#screen:, #sfx: 등)
                HandleTags();
            }
        }

        private void HandleTags()
        {
            foreach (string tag in story.currentTags)
            {
                if (tag.StartsWith("screen:"))
                {
                    string nextScene = tag.Substring("screen:".Length).Trim();
                    ScreenManager.Instance.ChangeScreen(nextScene);
                }
            }
        }
    }
}