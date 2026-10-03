using System.Collections.Generic;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance { get; private set; }

        
        public void Start()
        {
            // 첫 화면 로드 (예시)
            if (screenDatabase.Count > 0)
            {
                ChangeScreen(screenDatabase[0].sceneKnotName);
            }
        }

        [Header("화면 프리팹 등록")]
        public List<GameScreenView> screenDatabase = new();
        public Transform screenContainer;     // 화면이 스폰될 부모 Canvas/트랜스폼

        private GameObject _currentScreenObj;
        public GameScreenView CurrentScreenView { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        // Ink 태그를 통해 호출될 화면 전환 함수
        public void ChangeScreen(string screenId)
        {
            // 1. 기존 화면 제거
            if (_currentScreenObj != null)
            {
                Destroy(_currentScreenObj);
            }

            // 2. 등록된 프리팹 찾기
            GameScreenView data = screenDatabase.Find(s => s.sceneKnotName == screenId);
            if (data != null)
            {
                _currentScreenObj = Instantiate(data.gameObject, screenContainer);
                CurrentScreenView = _currentScreenObj.GetComponent<GameScreenView>();
            }
            else
            {
                Debug.LogWarning($"[ScreenManager] 화면 ID를 찾을 수 없음: {screenId}");
            }
        }
    }
}