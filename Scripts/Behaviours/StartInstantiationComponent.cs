using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ArChi
{
    public class StartInstantiationComponent : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private VoidEventChannel startGameChannel;
        [Space(20)]
        [Header("Data")]
        [SerializeField] private SpawnData spawnData;
        [Space(20)]
        [SerializeField] private GameObject scenary;
        [SerializeField] private Transform arCamera;
        [SerializeField] private ARPositionOffsetTracker arPositionTracker;
        [SerializeField] private ARRaycastManager arRaycastManager;
        [SerializeField] private ARPlaneManager arPlaneManager;
        ARSessionOrigin arSession;
        private Ray ray;
        private RaycastHit hit;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private Touch touch;

        private bool started;

        void Update()
        {
            if (started)
            {
                return;
            }
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                ARFloorRayCasting();
            }
#else

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ARFloorRayCasting();
            }
        }
#endif
        }
        private void StartGame()
        {
            Debug.Log("[Game] starting game");
            startGameChannel.TriggerEvent();
        }

        private void ARFloorRayCasting()
        {
            Ray ray = new Ray(arCamera.position, arCamera.forward);
#if UNITY_EDITOR
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                InstatiateScenary(hit.point);
            }
#else
            if (arRaycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                InstatiateScenary(hits[0].pose.position);
            }
#endif
        }
        private void InstatiateScenary(Vector3 position)
        {
            Debug.Log("[Game] Instantiating scenary");
            started = true;
#if !UNITY_EDITOR
            arPositionTracker.Origin = position;
#endif
            scenary.SetActive(true);
            //Instantiate(scenary, position, scenary.transform.rotation);
            arPlaneManager.enabled = false;
            arRaycastManager.enabled = false;
            Debug.Log("[Game] Destroying planes");

            foreach (var plane in arPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
            }

            Debug.Log("[Game] Destroying manages");
            Destroy(arPlaneManager);
            Destroy(arRaycastManager);

            Debug.Log("[Game] invoking start game");
            Invoke(nameof(StartGame), 2);
        }
    }

}
