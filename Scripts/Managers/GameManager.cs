using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ArChi
{
    public class GameManager : MonoBehaviour, IGame, IPlayerPositionChannel
    {
        [Header("Channels")]
        [SerializeField] private VoidEventChannel startGameChannel;
        [SerializeField] private VoidEventChannel endGameChannel;
        [SerializeField] private Vecto3DelegateChannel patrolPointChannel;
        [Space(20)]
        [Header("Controllers")]
        [SerializeField] private GameController controller;
        [Space(20)]
        [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
        [SerializeField] private GameObject scenary;

        private GameStatus status;
        private float playTime;

        public bool debug;

        public GameController Controller { get => controller; }

        private void Start()
        {
            if (debug)
            startGameChannel.TriggerEvent();
        }

        private void OnEnable()
        {
            controller.RegisterEvent();
            startGameChannel.eventChannel += StartGame;
            endGameChannel.eventChannel += EndGame;
            patrolPointChannel.listener += GetPatrolPoint;
        }
        private void OnDisable()
        {
            controller.UnregisterEvent();
            startGameChannel.eventChannel -= StartGame;
            endGameChannel.eventChannel -= EndGame;
            patrolPointChannel.listener -= GetPatrolPoint;
        }

        public void StartGame()
        {
            scenary.SetActive(true);
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
        
        private Vector3 GetPatrolPoint()
        {
            if (patrolPoints.Count == 0)
            {
                return Vector3.zero;
            }
            int selectedPoint = UnityEngine.Random.Range(0, patrolPoints.Count);
            Debug.Log($"[Patrol] selected point: {selectedPoint}, position: {patrolPoints[selectedPoint].position}");
            return patrolPoints[selectedPoint].position;
        }

        public void SetPlayerChannel(Vecto3DelegateChannel channel)
        {
            Controller.PlayerPositionChannel = channel;
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