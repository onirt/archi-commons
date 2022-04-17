using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static string RemoteAssetsServer;
    [SerializeField] private NetworkSetup setup;

    private void Awake()
    {
        RemoteAssetsServer = setup.RemoteAssetsServer;
    }
}
