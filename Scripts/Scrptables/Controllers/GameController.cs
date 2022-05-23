using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public abstract class GameController : ScriptableObject, IAddressableListHandle, IEventRegister, IGame
    {
        [Header("Base Class")]
        [Header("Channels")]
        [SerializeField] protected VoidEventChannel startGameChannel;
        [SerializeField] protected VoidEventChannel endGameChannel;
        [SerializeField] private FloatEventChannel scoreChannel;
        [Space(20)]
        [SerializeField] protected GameSetup setup;
        public List<string> addressables = new List<string>();
        [Space(40)]
        protected float score;

        public void AddScore(float value)
        {
            score += value;
        }
        public virtual void RegisterEvent()
        {
            startGameChannel.eventChannel += StartGame;
            scoreChannel.eventChannel += AddScore;
        }
        public virtual void UnregisterEvent()
        {
            startGameChannel.eventChannel -= StartGame;
            scoreChannel.eventChannel -= AddScore;
        }

        public void AddAddressable(string addressable)
        {
            addressables.Add(addressable);
        }

        public bool ContainsAddressable(string addressable)
        {
            return addressables.Contains(addressable);
        }

        public GameSetup Setup { get => setup; }

        public abstract void StartGame();
        public abstract void EndGame();
    }
}