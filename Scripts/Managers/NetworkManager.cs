using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public static string RemoteAssetsServer;
    [SerializeField] private NetworkSetup setup;

    public string RemoteCatalogAssetsPath { get => setup.RemoteCatalogAssetsPath; }

    private void Awake()
    {
        RemoteAssetsServer = setup.RemoteAssetsServer;
    }
    public void SetupFile(string setup_file, UnityAction<string> response)
    {
        GETHandler handler = new GETHandler($"{setup.RemoteSever}/{setup_file}?t={Random.Range(0,10000)}", response);
        StartCoroutine(Request(handler));
    }

    IEnumerator Request(IWWWHandler handler)
    {
        UnityWebRequest www = handler.GetRequest();

        Debug.Log($"[Network] url: {www.url}");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            handler.SetResponse(www.downloadHandler);
        }
    }

    public interface IWWWHandler
    {
        string GetEndPoint();
        UnityWebRequest GetRequest();
        void SetResponse(DownloadHandler downloadHandler);
        void SetError();
    }
    public abstract class WWWHandler : IWWWHandler
    {
        protected string endpoint;

        protected UnityAction error;

        public abstract string GetEndPoint();

        public abstract UnityWebRequest GetRequest();

        public abstract void SetError();

        public abstract void SetResponse(DownloadHandler downloadHandler);
    }
    public class GETHandler : WWWHandler
    {
        UnityAction<string> response;
        public GETHandler(string endpoint, UnityAction<string> response)
        {
            this.endpoint = endpoint;
            this.response = response;
        }

        public override string GetEndPoint()
        {
            return endpoint;
        }

        public override void SetError()
        {
            error?.Invoke();
        }

        public override UnityWebRequest GetRequest()
        {
            return UnityWebRequest.Get(endpoint);
        }

        public override void SetResponse(DownloadHandler downloadHandler)
        {
            response?.Invoke(downloadHandler.text);
        }
    }
}
