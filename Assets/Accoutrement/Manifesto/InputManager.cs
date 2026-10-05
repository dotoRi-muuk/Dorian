using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Accoutrement.Manifesto
{
    public class InputManager: MonoBehaviour
    {
        private DorInputSystem _dorInputSystem;

        private void OnEnable()
        {
            _dorInputSystem = new DorInputSystem();
            _dorInputSystem
        }
    }
}