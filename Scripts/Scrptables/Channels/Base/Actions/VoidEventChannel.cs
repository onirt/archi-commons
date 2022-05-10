using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ArChi
{
    [CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Channels/Events/Void")]
    public class VoidEventChannel : ScriptableObject
    {
        public UnityAction eventChannel;

        public void TriggerEvent()
        {
            eventChannel?.Invoke();
        }
    }
}
