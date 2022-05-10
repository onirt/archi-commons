using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ArChi
{
    public class SpawnController : ScriptableObject, IEventRegister
    {
        [Header("Channels")]
        [SerializeField] private IntEventChannel spawnChannel;

        [Header("Data")]
        [SerializeField] private SpawnData[] spawnsData;

        public UnityEvent<SpawnData> onSpawn;

        public void RegisterEvent()
        {
            spawnChannel.eventChannel += Spawn;
        }
        public void UnregisterEvent()
        {
            spawnChannel.eventChannel -= Spawn;
        }

        public void Spawn(int index)
        {
            if (index < spawnsData.Length)
            {
                onSpawn?.Invoke(spawnsData[index]);
            }
        }
    }
}