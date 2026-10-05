using System;
using Ink.Runtime;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    /*
     * 문서 내에서의 언급 정리
     * ### Event : Click on Object
     *
     * 버튼을 누르면 해당 Character은 InkyIntegrator을 호출.
     *
     * InkyIntegrator은 해당 버튼의 ID를 가진 Stitch로 커서를 이동. 읽을 수 있는 만큼 읽은 후 TextManager에 전달.
     *
     * ### Event : Click on Next Day Button
     *
     * 해당 NextDayButton은 InkyIntegrator을 호출.
     *
     * InkyIntegrator은 해당 버튼의 ID를 가진 Stitch로 커서를 이동. 읽을 수 있는 만큼 읽습니다.
     *
     * Next Day Button의 Stitch는 특별해서, 태그가 들어갈 수 있습니다. 태그를 읽으면 SceneManager에 전달합니다.
     */
    public class InkyIntegrator : MonoBehaviour
    {
        [SerializeField] private TextAsset ink;
        
        private Story _story;
        private String _currentKnot;
        private ProsceniumManager _prosceniumManager;
        public event Action<string> OnRead;
        public event Action<string> OnTag;

        public void Initialize(ProsceniumManager prosceniumManager)
        {
            _story = new Story(ink.text);
            if (!_story) throw new NullReferenceException("Story not initialized");
            _prosceniumManager = prosceniumManager;
        }

        public void StartReading(string stitchID)
        {
            var fullPath = $"{_prosceniumManager._currentScene.getSceneID}.{stitchID}";
            _story.ChoosePathString(fullPath);
            
            while (_story.canContinue)
            {
                var message = _story.Continue();
                OnRead?.Invoke(message);
                var storyCurrentTags = _story.currentTags;
                foreach (var storyCurrentTag in storyCurrentTags) OnTag?.Invoke(storyCurrentTag);
                
            }
        }
    }
}