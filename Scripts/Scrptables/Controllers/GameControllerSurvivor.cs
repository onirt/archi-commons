using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ArChi
{
    [CreateAssetMenu(fileName = "GameControllerSurvivor", menuName = "Controllers/Game/Survivor")]
    public class GameControllerSurvivor : GameController
    {
        [Header("Channels")]
        [SerializeField] private FloatEventChannel playerHealthChannel;
        [SerializeField] AIBehaviourEventChannel enemyDiedChannel;
        [SerializeField] SpawnPointChannel spawnPointChannel;

        [Space(20)]
        [SerializeField] private Attributes player;
        [SerializeField] private SpawnData[] spawnsData;

        private int diedsCount;
        private int round;

        public override void RegisterEvent()
        {
            base.RegisterEvent();
            enemyDiedChannel.eventChannel += EnemyDied;
        }
        public override void UnregisterEvent()
        {
            base.UnregisterEvent();
            enemyDiedChannel.eventChannel -= EnemyDied;
        }
        private void EnemyDied(AIBehaviour enemy)
        {

            if (enemy.TryGetComponent(out IScore _score))
            {
                score += _score.GetPoints();
            }
            diedsCount++;
            GameSetupSurvivor survivorSetup = (GameSetupSurvivor)setup;
            if (diedsCount >= survivorSetup.GetTarget(round))
            {
                EndGame();
            }
            else
            {
                if (enemy.TryGetComponent(out ISpawer _spawner))
                {
                    _spawner.ReSpawn();
                }
            }
        }
        public async override void EndGame()
        {
            endGameChannel.TriggerEvent();

            round++;
            if (round < setup.Rounds)
            {
                await Task.Delay(5);
                startGameChannel.TriggerEvent();
            }

        }

        public override void StartGame()
        {
            Debug.Log($"[Game] controller start... round [{round}]");
            GameSetupSurvivor survivorSetup = (GameSetupSurvivor)setup;
            int size = survivorSetup.goals[round] / 2;

            SpawnData spawnData;
            for (int i = 0; i < size; i++)
            {
                spawnData = spawnsData[Random.Range(0, spawnsData.Length)];
                spawnData.Instantiate(spawnPointChannel.Get(spawnData.type), round, Spawned);
            }
        }

        void Spawned(GameObject spawned)
        {
            Debug.Log($"[Game] spawned succes: [{spawned.name}]");
        }
    }
}
