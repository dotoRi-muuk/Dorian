using System;
using Ink.Runtime;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    public class InkyIntegrator : MonoBehaviour
    {
        private static InkyIntegrator _instance;

        public static InkyIntegrator Instance => _instance;

        [Header("Singleton Settings")]
        [SerializeField] private bool dontDestroyOnLoad = true;

        [SerializeField] private TextAsset message;
        [SerializeField] private TextManager textManager;
        private Story _story;
        private String _currentKnot;

        private void OnEnable()
        {
            _story = new Story(message.text);
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                if (dontDestroyOnLoad)
                {
                    // 최상위 루트 오브젝트여야 DontDestroyOnLoad가 정상 작동
                    transform.SetParent(null);
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (_instance != this)
            {
                // 중복 생성된 오브젝트는 즉시 제거
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            // 자신이 해제될 때 정적 참조도 함께 정리
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public void Jump(String id)
        {
            
        }
    }
}