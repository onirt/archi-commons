using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class LookAtCameraBehaviour : MonoBehaviour, IPlayerPositionChannel
    {
        [SerializeField] private Vecto3DelegateChannel playerChannel;

        void Update()
        {
            if (playerChannel)
            {
                transform.LookAt(playerChannel.Get());
                transform.Rotate(0,180,0);
            }
        }
        public void SetPlayerChannel(Vecto3DelegateChannel channel)
        {
            playerChannel = channel;
        }
    }
}
