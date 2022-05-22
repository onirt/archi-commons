using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{

    [CreateAssetMenu(fileName = "TransformDelegateChannel", menuName = "Channels/Delegates/Transform")]
    public class SpawnPointChannel : ScriptableObject
    {
        public delegate Transform GetListener(SpawnType type);
        public GetListener listener;

        public Transform Get(SpawnType type)
        {
            if (listener == null)
            {
                Debug.Log($"[Error] no body is liten to me :( [{name}]");
            }
            return listener(type);
        }
    }
}
