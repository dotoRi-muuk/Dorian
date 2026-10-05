using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    /*
     * 문서 내에서의 언급 정리
     *
     * ### Event : Click on Object
     *
     * InkyIntegrator은 해당 버튼의 ID를 가진 Stitch로 커서를 이동. 읽을 수 있는 만큼 읽은 후 TextManager에 전달.
     *
     * TextManager은 전달받았을 때 입력 제어를 시작. 텍스트를 띄웁니다. 텍스트를 띄울 때와 다음 텍스트로 넘어갈 때, 그리고 대화가 전부 끝날 때 SceneManager을 호출합니다. V
     *
     * ### Event : Click on Control
     *
     * 해당 InputManager은 TextManager을 호출합니다.
     *
     * TextManager의 행동은 텍스트 출력 중 / 텍스트 출력 후가 나뉩니다. 극적인 연출을 위해서는 이때의 입력도 제어해야 합니다만, 방법은 모르겠습니다.
     *
     */

    public class TextManager : MonoBehaviour
    {
        [SerializeField] private InkyIntegrator inkyIntegrator;
        [SerializeField] private GameObject inputBlocker;
        [SerializeField] private TextMeshProUGUI authorText;
        [SerializeField] private TextMeshProUGUI messageText;
        private Queue<string> _messageQueue = new();
        private bool _isWriting = false;
        private bool isTexting => inputBlocker.activeInHierarchy;

        public event Action<string> CameraFocus;

        public void Initialize(InputManager inputManager, InkyIntegrator integrator)
        {
            inputManager.OnClick += OnClick;
            integrator.OnRead += EnqueueMessage;
        }

        private void EnqueueMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return;
            _messageQueue.Enqueue(message);
            NextMessage();
        }

        private void OnClick()
        {
            if (_isWriting)
            {
                throw new NotImplementedException();
            }
            else if(isTexting)
            {
                NextMessage();
            }
        }

        private void NextMessage()
        {
            if (_messageQueue.Count > 0)
            {
                inputBlocker.SetActive(true);
                var rawMessage = _messageQueue.Dequeue();
                if (!rawMessage.Contains(":"))
                {
                    throw new Exception("Invalid message");
                }

                var strings = rawMessage.Split(":");
                authorText.text = strings[0];
                messageText.text = strings[1];
                CameraFocus?.Invoke(strings[0]);
            }
            else
            {
                inputBlocker.SetActive(false);
                CameraFocus?.Invoke(null);
            }
        }
    }
}