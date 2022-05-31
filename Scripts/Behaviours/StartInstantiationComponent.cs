using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.XR.ARFoundation;

namespace ArChi
{
    public class StartInstantiationComponent : MonoBehaviour, IAddressableListHandle
    {
        [Header("Channels")]
        [SerializeField] private Vecto3DelegateChannel playerPositionChannel;
        [Space(20)]
        [Header("Data")]
        [SerializeField] private SpawnData spawnData;
        [Space(20)]
        [SerializeField] private string category;
        [SerializeField] private string addressableGame;
        [SerializeField] private Camera arCamera;
        [SerializeField] private Transform arCameraTransform;
        [SerializeField] private ARPositionOffsetTracker arPositionTracker;
        [SerializeField] private ARRaycastManager arRaycastManager;
        [SerializeField] private ARPlaneManager arPlaneManager;
        private ARSessionOrigin arSession;
        private Ray ray;
        private RaycastHit hit;
        private List<ARRaycastHit> hits = new List<ARRaycastHit>();
        private Touch touch;
        private UnityEvent startGame = new UnityEvent();

        private bool started;
        private AsyncOperationHandle<GameObject> handle;

        private async void Start()
        {
            Debug.Log("[Start] starting load minigame");
            handle = Addressables.LoadAssetAsync<GameObject>(addressableGame);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject scenary = Instantiate(handle.Result);
                Debug.Log($"[Start] minigame loaded success [{scenary.name}]");
                arPositionTracker.Sceneary = scenary.transform;
                SetSetup(scenary);
                GetInputEvent();
            }
            else
            {
                Debug.LogError("[Start][Addressable] mingane load fail");
            }
        }
        private void OnDestroy()
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
            startGame.RemoveAllListeners();
        }
        private async void SetSetup(GameObject minigame)
        {
            Debug.Log($"1.-[Start] setting minigame [{minigame.name}]");
            if (minigame.TryGetComponent(out IGame game))
            {
                startGame.AddListener(game.StartGame);
            }
            Debug.Log($"2.-[Start] setting minigame [{minigame.name}]");
            if (minigame.TryGetComponent(out IPlayerPositionChannel channel))
            {
                channel.SetPlayerChannel(playerPositionChannel);
            }
            Debug.Log($"3.-[Start] setting minigame [{minigame.name}]");
            if (minigame.TryGetComponent(out ICamera camera))
            {
                camera.SetCamera(arCamera);
            }
            Debug.Log($"4.-[Start] setting minigame [{minigame.name}]");
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
            startGame?.Invoke();
        }

        private void ARFloorRayCasting()
        {
            Ray ray = new Ray(arCameraTransform.position, arCameraTransform.forward);
#if UNITY_EDITOR
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                InstatiateScenary(hit.point);
            }
#else
            /*if (arRaycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                InstatiateScenary(hits[0].pose.position);
            }*/

            InstatiateScenary(transform.position);
#endif
        }
        private void InstatiateScenary(Vector3 position)
        {
            Debug.Log("[Game][Start] Instantiating scenary");
            started = true;

            arPositionTracker.Origin = position;

            /*arPlaneManager.enabled = false;
            arRaycastManager.enabled = false;
            Debug.Log("[Game][Start] Destroying planes");

            foreach (var plane in arPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
            }*/

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
