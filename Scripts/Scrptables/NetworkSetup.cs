using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "NetworkSetup", menuName = "Network/Setup")]
    public class NetworkSetup : ScriptableObject
    {

        public string versionFolder;
        [SerializeField] private string catalog;
        [Space(20)]
        [Header("Platforms")]
        [SerializeField] string androidPlatform = "Android";
        [SerializeField] string iOSPlatform = "iOS";
        [SerializeField] string windowsPlatform = "StandaloneWindows";
        [SerializeField] string macOSPlatform = "StandaloneOSX";

        [Space(20)]
        [SerializeField] private string remoteAssetsServer = "";

        public string RemoteSever { get => $"{remoteAssetsServer}/{versionFolder}"; }
        public string RemoteAssetsServer { get => $"{remoteAssetsServer}/{versionFolder}/{GetPlatform()}"; }
        public string RemoteCatalogAssetsPath { get => $"{remoteAssetsServer}/{versionFolder}/{GetPlatform()}/{catalog}?t={Random.Range(0, 10000)}"; }

        public string GetPlatform()
        {
            Debug.Log($"[Platform] {Application.platform}");
            if (Application.platform == RuntimePlatform.Android)
            {
                return androidPlatform;
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                return windowsPlatform;
            }
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                return macOSPlatform;
            }
            return iOSPlatform;
        }
    }
}