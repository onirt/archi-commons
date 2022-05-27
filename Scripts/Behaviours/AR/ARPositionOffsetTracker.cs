using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ArChi
{
    public class ARPositionOffsetTracker : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private FloatDelegateChannel worldScaleChannel;
        [Space(20)]
        [SerializeField] private Transform arCamera;
        [SerializeField] private ARSessionOrigin arSessionOrigin;
        [SerializeField] private Transform scenary;
        [SerializeField] private Vector3 origin;

        public Vector3 Origin { 
            set { 
                origin = value;
                SetOrigin();
            } 
        }

        private void SetOrigin()
        {
            Debug.Log($"[Start] setting origing scale: [{worldScaleChannel.Get()}], origin: {origin}, camera position: {arCamera.position}");
            origin -= arCamera.forward * worldScaleChannel.Get();
            //arSessionOrigin.MakeContentAppearAt(scenary, origin);
            arCamera.position += origin;
            Debug.Log($"[Start] final camera position: {arCamera.position}");
        }
    }
}
