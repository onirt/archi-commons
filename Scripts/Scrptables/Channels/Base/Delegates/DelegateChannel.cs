using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class DelegateChannel<T> : ScriptableObject
    {

        [SerializeField] private T defaultValue;
        public delegate T GetListener();
        public GetListener listener;

        public T Get()
        {
            if (listener == null)
            {
                Debug.Log($"[Error] no listener detected on {name}");
                return defaultValue;
            }
            return listener();
        }
    }
}
