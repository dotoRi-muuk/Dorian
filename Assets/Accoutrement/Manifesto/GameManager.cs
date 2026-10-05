using System;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    /*
     * 문서 내에서의 언급 정리 V
     *
     * ### Event : Click on Next Day Button
     *
     * SceneManager은 해당 태그를 읽고, 현재의 씬을 제거, 다음 Knot의 프리팹을 읽습니다. 이후 각각의 Character들을 <string, Character>의 맵으로 읽어냅니다. 만약 태그가 END일 시, 이전의 동작을 수행하지 않고 GameManager.END를 호출합니다. V
     *
     * ### Event : Start
     *
     * GameManager은 SceneManager에 상수의 씬을 로드 호출합니다. V
     */

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ProsceniumManager prosceniumManager;
        [SerializeField] private TextManager textManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private InkyIntegrator integrator;

        [SerializeField] private string initialScene;

        private void Start()
        {
            inputManager.Initialize();

            textManager.Initialize(inputManager, integrator);

            prosceniumManager.Initialize(textManager, integrator);
            prosceniumManager.OnGameEnd += End;

            integrator.Initialize(prosceniumManager);


            prosceniumManager.LoadScene(initialScene);
        }

        private void End()
        {
            throw new NotImplementedException();
        }
    }
}