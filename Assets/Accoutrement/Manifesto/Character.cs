using System;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    /*
     * 문서 내에서의 언급 정리 V
     *
     * 1
     * Character은 ID를 가집니다. 이쪽은 가독성을 위한 거 V
     *
     * 2
     * ### Event : Click on Object
     * 버튼을 누르면 해당 Character은 InkyIntegrator을 호출.
     */
    
    public class Character: MonoBehaviour
    {
        
        //1
        [SerializeField] private string id;
        private InkyIntegrator _inkyIntegrator;

        public void Initialize(InkyIntegrator inkyIntegrator)
        {
            _inkyIntegrator = inkyIntegrator;
        }

        public string GetId()
        {
            return id;
        }
        
        //2
        private void Send()
        {
            _inkyIntegrator.StartReading(id);
        }
    }
}