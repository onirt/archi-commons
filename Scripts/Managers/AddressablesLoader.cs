using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace ArChi
{
    public class AddressablesLoader : MonoBehaviour
    {
        [SerializeField] private UnityEvent<string> loadAddressable ;

        public void LoadAddressable(string addressable)
        {
            loadAddressable?.Invoke(addressable);
        }

        public async void LoadingAddressables(string addressable)
        {
            var handler = Addressables.LoadAssetAsync<GameObject>(addressable);
            await handler.Task;
        }
    }
}
