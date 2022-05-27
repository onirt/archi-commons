using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class PlayerPositionTrackerComponent : MonoBehaviour
    {
        [SerializeField] private Vecto3DelegateChannel playerPositionChannel;

        private void OnEnable()
        {
            playerPositionChannel.listener += GetPosition;
        }
        private void OnDisable()
        {
            playerPositionChannel.listener -= GetPosition;
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}
