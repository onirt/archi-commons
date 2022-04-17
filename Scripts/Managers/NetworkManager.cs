using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static string RemoteAssetsServer;
    [SerializeField] private NetworkSetup setup;

    public string RemoteCatalogAssetsPath { get => setup.RemoteCatalogAssetsPath; }

    private void Awake()
    {
        RemoteAssetsServer = setup.RemoteAssetsServer;
    }
}
