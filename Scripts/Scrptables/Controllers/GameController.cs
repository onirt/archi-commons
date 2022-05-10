using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public abstract class GameController : ScriptableObject, IEventRegister, IGame
    {
        [Header("Channels")]
        [SerializeField] protected VoidEventChannel startGameChannel;
        [SerializeField] protected VoidEventChannel endGameChannel;
        [SerializeField] private FloatEventChannel scoreChannel;
        [Space(20)]
        [SerializeField] protected GameSetup setup;

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

        public GameSetup Setup { get => setup; }

        public abstract void StartGame();
        public abstract void EndGame();
    }
}