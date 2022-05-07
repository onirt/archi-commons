using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IAddressableListHandle, IGame
{
    [SerializeField] GameController controller;
    public List<string> addressables = new List<string>();

    private float playTime;

    private void OnEnable()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(Playing());
    }
    public void EndGame()
    {

    }

    IEnumerator Playing()
    {
        playTime = 0;
        while (playTime < controller.Setup.GameTime)
        {
            playTime += Time.deltaTime;
            yield return null;
        }
    }

    public void AddAddressable(string addressable)
    {
        addressables.Add(addressable);
    }

    public bool ContainsAddressable(string addressable)
    {
        return addressables.Contains(addressable);
    }
}
