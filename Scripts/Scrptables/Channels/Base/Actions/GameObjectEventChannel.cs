using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "GameObjectEventChannel", menuName = "Channels/Events/GameObject")]
    public class GameObjectEventChannel : EventChannel<GameObject>
    {
    }
}
