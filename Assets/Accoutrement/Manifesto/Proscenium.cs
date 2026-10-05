using System;
using System.Collections.Generic;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    public class Proscenium: MonoBehaviour
    {
        [SerializeField] private string sceneID;

        private Dictionary<string, Character> _actorDict;

        public void Load(InkyIntegrator inkyIntegrator)
        {
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out Character character)) continue;
                _actorDict[character.GetId()] = character;
                character.Initialize(inkyIntegrator);
            }
        }

        public Character GetActor(string id)
        {
            return _actorDict[id];
        }

        public string getSceneID => sceneID;
    }
}