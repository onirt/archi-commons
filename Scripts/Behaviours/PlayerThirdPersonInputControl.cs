using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class PlayerThirdPersonInputControl : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private Vector3EventChannel destinationChannel;
        [SerializeField] private TransformEventChannel attackChannel;
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
                    if (hit.collider.TryGetComponent(out ITakeDamage take))
                    {
                        attackChannel.TriggerEvent(hit.transform);
                    }
                    else
                    {
                        destinationChannel.TriggerEvent(hit.point);
                    }
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
                    if (hit.collider.TryGetComponent(out ITakeDamage take))
                    {
                        attackChannel.TriggerEvent(hit.transform);
                    }
                    else
                    {
                        destinationChannel.TriggerEvent(hit.point);
                    }
                }
            }
        }
#endif
        }
    }
}
