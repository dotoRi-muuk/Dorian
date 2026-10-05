using System;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    public class Character: MonoBehaviour
    {
        [SerializeField] private string id;
        private void Send()
        {
            InkyIntegrator.Instance.Jump(id);
        }
    }
}