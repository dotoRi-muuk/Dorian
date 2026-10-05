using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Accoutrement.Manifesto
{
    public struct MessageInfo
    {
        public MessageInfo(string author, string message)
        {
            Author = author;
            Message = message;
        }

        public string Author { get; }
        public string Message { get; }
    }

    public class TextManager: MonoBehaviour
    {
        private Queue<MessageInfo> _queue;
        public bool AnythingElse => _queue.Count > 0;
        public MessageInfo NextPhrase => AnythingElse ? _queue.Dequeue() : new MessageInfo();
        
        private void OnEnable()
        {
            _queue = new Queue<MessageInfo>();
        }

        public void Enqueue(String message)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (message.Contains(":"))
            {
                var strings = message.Split(":");
                var author = strings[0];
                var text = strings[1];
                _queue.Enqueue(new MessageInfo(author, text));
            }
            else _queue.Enqueue(new MessageInfo(string.Empty, message));
        }
    }
}