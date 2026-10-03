using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    public class GameScreenView : MonoBehaviour
    {
        public string sceneKnotName = "Scene_1"; // 현재 씬의 Knot 이름
        public TextMeshProUGUI speakerNameText;
        public TextMeshProUGUI dialogueText;

        // 현재 화면에 올려진 캐릭터들
        public List<CharacterView> charactersInScene = new List<CharacterView>();

        // 화자에 따른 캐릭터 하이라이트 갱신
        public void UpdateCharacterFocus(string currentSpeakerId)
        {
            foreach (var ch in charactersInScene)
            {
                // 화자 ID와 일치하면 하얗게, 아니면 어둡게
                bool isMe = (!string.IsNullOrEmpty(currentSpeakerId) && ch.characterId == currentSpeakerId);
                ch.SetHighlight(isMe);
            }
        }
    }
}