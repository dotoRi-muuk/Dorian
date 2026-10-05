using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    /*
     * 문서 내에서의 언급 정리
     *
     * ### Event : Click on Object
     *
     * TextManager은 전달받았을 때 입력 제어를 시작. 텍스트를 띄웁니다. 텍스트를 띄울 때와 다음 텍스트로 넘어갈 때, 그리고 대화가 전부 끝날 때 ProsceniumManager을 호출합니다.
     *
     * ProsceniumManager은 해당 발화자를 돋보이게 합니다. V
     *
     * ### Event : Click on Next Day Button
     *
     * Next Day Button의 Stitch는 특별해서, 태그가 들어갈 수 있습니다. 태그를 읽으면 ProsceniumManager에 전달합니다.
     *
     * ProsceniumManager은 해당 태그를 읽고, 현재의 씬을 제거, 다음 Knot의 프리팹을 읽습니다. 이후 각각의 Character들을 <string, Character>의 맵으로 읽어냅니다. 만약 태그가 END일 시, 이전의 동작을 수행하지 않고 GameManager.END를 호출합니다. V
     *
     * ### Event : Start
     *
     * GameManager은 ProsceniumManager에 상수의 씬을 로드 호출합니다. V
     */
    public class ProsceniumManager : MonoBehaviour
    {
        [SerializeField] private List<Proscenium> scenePrefab;
        [SerializeField] private CinemachineCamera characterCamera;
        public Proscenium _currentScene;

        private Dictionary<string, Proscenium> sceneMap = new();
        private InkyIntegrator _inkyIntegrator;

        public event Action OnGameEnd;

        public void Initialize(TextManager textManager,  InkyIntegrator inkyIntegrator)
        {
            foreach (var proscenium in scenePrefab)
            {
                sceneMap[proscenium.getSceneID] = proscenium;
            }
            
            _inkyIntegrator = inkyIntegrator;
            inkyIntegrator.OnTag += CheckTag;
            textManager.CameraFocus += CameraFocus;
        }

        private void CheckTag(string ctx)
        {
            var strings = ctx.Split(":");
            var tagType = strings[0];
            var sceneName = strings[1];
            if (tagType == "screen") LoadScene(sceneName);
        }

        public void LoadScene(string sceneName)
        {
            if (string.Equals(sceneName, "end", StringComparison.OrdinalIgnoreCase))
            {
                OnGameEnd?.Invoke();
                return;
            }

            if (sceneMap.TryGetValue(sceneName, out var proscenium))
            {
                if (_currentScene) Destroy(_currentScene); //Unloading
                Instantiate(proscenium.gameObject, Vector3.zero, Quaternion.identity)
                    .TryGetComponent(out _currentScene); //Loading
                if (_currentScene) _currentScene.Load(_inkyIntegrator);
            }
            else
            {
                throw new Exception($"Scene {sceneName} not found");
            }
        }

        private void CameraFocus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                characterCamera.Priority = 0;
                return;
            }

            characterCamera.Priority = 200;
            characterCamera.Follow = _currentScene.GetActor(id).transform;
        }
    }
}