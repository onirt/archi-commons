using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.XR.ARFoundation;

namespace ArChi
{
    public class StartInstantiationComponent : MonoBehaviour, IAddressableListHandle
    {
        [Header("Channels")]
        [SerializeField] private VoidEventChannel startGameChannel;
        [SerializeField] private Vecto3DelegateChannel playerPositionChannel;
        [Space(20)]
        [Header("Data")]
        [SerializeField] private SpawnData spawnData;
        [Space(20)]
        [SerializeField] private string category;
        [SerializeField] private string addressableGame;
        [SerializeField] private Transform arCamera;
        [SerializeField] private ARPositionOffsetTracker arPositionTracker;
        [SerializeField] private ARRaycastManager arRaycastManager;
        [SerializeField] private ARPlaneManager arPlaneManager;
        private ARSessionOrigin arSession;
        private Ray ray;
        private RaycastHit hit;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private Touch touch;

        private bool started;

        private async void Start()
        {
            Debug.Log("[Start] starting load minigame");
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(addressableGame);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("[Start] minigame loaded success");
                SetSetup(Instantiate(handle.Result));
                GetInputEvent();
            }
            else
            {
                Debug.LogError("[Start][Addressable] mingane load fail");
            }
            Addressables.Release(handle);
        }
        private async void SetSetup(GameObject minigame)
        {
            if (minigame.TryGetComponent(out GameManager gameManager))
            {
                if (gameManager.Controller.Setup.requiereCameraTraking){
                    if (minigame.TryGetComponent(out IPlayerPositionChannel channel))
                    {
                        channel.SetPlayerChannel(playerPositionChannel);
                    }
                }
            }
            await Task.Yield();
        }
        private async void GetInputEvent()
        {
            Debug.Log("[Start][Input] starting input event");
#if UNITY_EDITOR
            while (!Input.GetMouseButton(0))
            {
                await Task.Yield();
            }
            ARFloorRayCasting();
#else

            while (!started)
            {
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        Debug.Log("[Start][Input] touch began");
                        ARFloorRayCasting();
                    }
                }
                await Task.Yield();
            }
#endif

            Debug.Log("[Start][Input] ending input event");
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
            Debug.Log("[Game][Start] Instantiating scenary");
            started = true;
            
            arPositionTracker.Origin = position;

            arPlaneManager.enabled = false;
            arRaycastManager.enabled = false;
            Debug.Log("[Game][Start] Destroying planes");

            foreach (var plane in arPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
            }

            Debug.Log("[Game][Start] Destroying manages");
            Destroy(arPlaneManager);
            Destroy(arRaycastManager);

            Debug.Log("[Game] invoking start game");
            Invoke(nameof(StartGame), 2);
        }

        public void AddAddressable(string addressable)
        {
            addressableGame = addressable;
        }

        public bool ContainsAddressable(string addressable)
        {
            return addressableGame.Equals(addressable);
        }

        public string GetFilter()
        {
            return category;
        }

        public void SetFilter(string category)
        {
            this.category = category;
        }
    }

}
