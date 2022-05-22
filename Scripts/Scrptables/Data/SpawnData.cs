using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ArChi
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "Data/Spawn")]
    public class SpawnData : ScriptableObject, IAddressableListHandle
    {
        public string category;
        public SpawnType type;

        public List<string> addressables = new List<string>();

        public Dictionary<string, AsyncOperationHandle<GameObject>> loaded = new Dictionary<string, AsyncOperationHandle<GameObject>>();

        private void OnDisable()
        {
            Dispose();
        }
        public void Dispose()
        {
            foreach (var handle in loaded)
            {
                Addressables.Release(handle.Value);
            }
            loaded.Clear();
        }
        public void AddAddressable(string addressable)
        {
            addressables.Add(addressable);
        }

        public bool ContainsAddressable(string addressable)
        {
            return addressables.Contains(addressable);
        }

        public void Instantiate(Transform point, int level, UnityAction<GameObject> response)
        {
            int selected = Random.Range(0, addressables.Count);
            Debug.Log($"[Game] instantiating [{addressables[selected]}]");
            Instantiate(addressables[selected], point, SpawnMode.Parent, level, response);
        }

        public async void Instantiate(string addressable, Transform transform, SpawnMode mode, int level, UnityAction<GameObject> response)
        {
            if (loaded.ContainsKey(addressable))
            {
                Debug.Log($"[Game] is already loaded [{addressable}]");
                InstantiatePrefab(addressable, transform, mode, response);
            }
            else
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(addressable);

                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"[Game] loaded success [{addressable}].. key count: {loaded.Count}");
                    if (!loaded.ContainsKey(addressable))
                    {
                        loaded.Add(addressable, handle);
                    }
                    InstantiatePrefab(addressable, transform, mode, response);
                }
                else
                {
                    Debug.Log($"[Game] Oh man, i couln't load this guy: [{addressable}]");
                    Addressables.Release(handle);
                }
            }
        }
        private void InstantiatePrefab(string addressable, Transform transform, SpawnMode mode, UnityAction<GameObject> response)
        {
            GameObject result;
            switch(mode)
            {
                case SpawnMode.Parent:
                    result = Instantiate(loaded[addressable].Result, transform);
                    break;
                case SpawnMode.Local:
                    result = Instantiate(loaded[addressable].Result, Vector3.zero, Quaternion.identity);
                    Transform resulTransform = result.transform;
                    resulTransform.localPosition = transform.localPosition;
                    resulTransform.localRotation = transform.localRotation;
                    break;
                default:
                    result = Instantiate(loaded[addressable].Result, transform.position, transform.rotation);
                    break;
            }
            response?.Invoke(result);
        }
    }
    public enum SpawnType
    {
        Player,
        GroundHumanoid,
        FlyHumanoid,
        GroundVehicle,
        FlyVehicle,
        GroundRobot,
        FlyRobot,
        Weapon
    }
    public enum SpawnMode
    {
        None,
        Parent,
        Local
    }
}