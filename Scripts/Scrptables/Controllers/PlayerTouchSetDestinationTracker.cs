using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class PlayerTouchSetDestinationTracker : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private Vector3EventChannel destinationChannel;
        [Space(20)]
        [SerializeField] private Camera testCamera;

        private Ray ray;
        private RaycastHit hit;
        private Touch touch;
        // Update is called once per frame
        void Update()
        {

#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                ray = testCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    destinationChannel.TriggerEvent(hit.point);
                }
            }
#else

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ray = testCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                {
                
                    Debug.Log($"[Game][Touch] setting destination: {hit.point}");
                    destinationChannel.TriggerEvent(hit.point);
                }
            }
        }
#endif
        }
    }
}
