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

        public Transform Sceneary { set => scenary = value; }
        public Vector3 Origin { 
            set { 
                origin = value;
                SetOrigin();
            } 
        }

        private void SetOrigin()
        {
            Debug.Log($"[Start] setting origing scale: [{worldScaleChannel.Get()}], origin: {origin}, camera position: {arCamera.position}");
            scenary.position = origin;
            origin -= Vector3.up * worldScaleChannel.Get();
            Debug.Log($"[Start] final origin: {origin}, camera position: {arCamera.position}, {arCamera.localPosition}");
            arSessionOrigin.MakeContentAppearAt(scenary, origin);
            
            //arCamera.position += origin;
            Debug.Log($"[Start] final camera position: {arCamera.position}, {arCamera.localPosition}");
        }
    }
}
