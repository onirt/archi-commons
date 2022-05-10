using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "TransformEventChannel", menuName = "Channels/Events/Transform")]
    public class TransformEventChannel : EventChannel<Transform>
    {
    }
}
