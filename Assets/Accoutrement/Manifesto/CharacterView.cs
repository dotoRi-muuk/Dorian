using UnityEngine;
using UnityEngine.UI;

namespace Accoutrement.Manifesto
{
    public class CharacterView : MonoBehaviour
    {
        [Header("캐릭터 고유 ID (Ink의 Stitch 접미사와 일치: c1, c2 등)")]
        public string characterId;

        [Header("비주얼 제어 대상")]
        public Image characterImage; // 2D SpriteRenderer라면 SpriteRenderer로 변경

        private Color normalColor = Color.white;
        private Color dimmedColor = new Color(0.4f, 0.4f, 0.4f, 1f); // 어둡게 처리할 색상

        // 캐릭터 버튼이 클릭되었을 때 호출 (Button OnClick에 연결)
        public void OnClickCharacter()
        {
            // DialogueController로 "btn_c1" 형태로 전달
            DialogueController.Instance.TriggerInteraction($"btn_{characterId}");
        }

        // 발화 여부에 따른 하이라이트/딤 처리
        public void SetHighlight(bool isSpeaking)
        {
            if (characterImage != null)
            {
                characterImage.color = isSpeaking ? normalColor : dimmedColor;
            }
        }
    }
}