using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "AIBehaviourEventChannel", menuName = "Channels/Events/AIBehaviour")]
    public class AIBehaviourEventChannel : EventChannel<AIBehaviour>
    {
    }
}