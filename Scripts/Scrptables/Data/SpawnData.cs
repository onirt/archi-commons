using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Data/Spawn")]
public class SpawnData : ScriptableObject, IAddressableListHandle
{
    public string type;

    public List<string> addressables = new List<string>();

    public Dictionary<int, AsyncOperationHandle<GameObject>> loaded = new Dictionary<int, AsyncOperationHandle<GameObject>>();

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

    public IEnumerator Instantiate(Transform point, int level, UnityAction<GameObject> response)
    {
        int selected = Random.Range(0, addressables.Count);
        if (loaded.ContainsKey(selected))
        {
            response?.Invoke(Instantiate(loaded[selected].Result, point.position, point.rotation));
        }
        else
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(addressables[selected]);
            while (!handle.IsDone)
            {
                yield return null;
            }
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                response?.Invoke(Instantiate(handle.Result, point.position, point.rotation));
                loaded.Add(selected, handle);
            }
            else
            {
                Addressables.Release(handle);
            }
        }
    }
}
