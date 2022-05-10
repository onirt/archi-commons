using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class GameManager : MonoBehaviour, IAddressableListHandle, IGame
    {
        [Header("Channels")]
        [SerializeField] private VoidEventChannel startGameChannel;
        [SerializeField] private VoidEventChannel endGameChannel;
        [Space(20)]
        [SerializeField] GameController controller;
        public List<string> addressables = new List<string>();

        private GameStatus status;
        private float playTime;

        private void Start()
        {
            startGameChannel.TriggerEvent();
        }

        private void OnEnable()
        {
            controller.RegisterEvent();
            startGameChannel.eventChannel += StartGame;
            endGameChannel.eventChannel += EndGame;
        }
        private void OnDisable()
        {
            controller.UnregisterEvent();
            startGameChannel.eventChannel -= StartGame;
            endGameChannel.eventChannel -= EndGame;
        }

        public void StartGame()
        {
            Debug.Log("[Game] start");
            status = GameStatus.Started;
            StartCoroutine(Playing());
        }
        public void EndGame()
        {
            Debug.Log("[Game] end");
            status = GameStatus.Ended;
        }

        IEnumerator Playing()
        {
            playTime = 0;
            while (status == GameStatus.Started && playTime < controller.Setup.GameTime)
            {
                playTime += Time.deltaTime;
                yield return null;
            }
            if (status == GameStatus.Started)
            {
                endGameChannel.TriggerEvent();
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
    public enum GameStatus
    {
        Started,
        Ended
    }
    [Serializable]
    public struct SpawnPoint
    {
        public Transform point;
        public SpawnType type;
    }
}