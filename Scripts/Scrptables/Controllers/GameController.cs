using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public abstract class GameController : ScriptableObject, 
        IAddressableListHandle, 
        IEventRegister, 
        IGame
    {
        [Header("Base Class")]
        [Header("Channels")]
        [SerializeField] protected GameObjectEventChannel gameConnectorChannel;
        [SerializeField] protected VoidEventChannel startGameChannel;
        [SerializeField] protected VoidEventChannel endGameChannel;
        [SerializeField] private FloatEventChannel scoreChannel;
        [SerializeField] private Vecto3DelegateChannel playerPositionChannel;
        [Space(20)]
        [SerializeField] private string category;
        [SerializeField] protected GameSetup setup;
        public List<string> addressables = new List<string>();
        [Space(40)]
        protected float score;

        public GameSetup Setup { get => setup; }
        public Vecto3DelegateChannel PlayerPositionChannel { set => playerPositionChannel = value; get => playerPositionChannel; }

        public void AddScore(float value)
        {
            score += value;
        }
        public virtual void RegisterEvent()
        {
            startGameChannel.eventChannel += StartGame;
            scoreChannel.eventChannel += AddScore;
            gameConnectorChannel.eventChannel += ConnectGameObject;
        }
        public virtual void UnregisterEvent()
        {
            startGameChannel.eventChannel -= StartGame;
            scoreChannel.eventChannel -= AddScore;
            gameConnectorChannel.eventChannel -= ConnectGameObject;
        }

        public void AddAddressable(string addressable)
        {
            addressables.Add(addressable);
        }

        public bool ContainsAddressable(string addressable)
        {
            return addressables.Contains(addressable);
        }

        public string GetFilter()
        {
            return category;
        }

        public void SetFilter(string filter)
        {
            category = filter;
        }

        public void ConnectGameObject(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IPlayerPositionChannel channel))
            {
                channel.SetPlayerChannel(playerPositionChannel);
            }
        }

        public abstract void StartGame();
        public abstract void EndGame();
    }
}