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
        [SerializeField] private Vector3EventChannel destinationChannel;
        [SerializeField] private TransformEventChannel attackChannel;
        [SerializeField] private FloatDelegateChannel worldScaleChannel;
        [SerializeField] private FloatEventChannel playerHealthChannel;
        [SerializeField] private AIBehaviourEventChannel enemyDiedChannel;
        [SerializeField] private SpawnPointChannel spawnPointChannel;
        [Space(20)]
        [SerializeField] private SpawnData[] playersSpawnsData;
        [SerializeField] private SpawnData[] enemysSpawnsData;
        [Space(20)]
        [SerializeField] private Attributes player;
        [Space(20)]
        [SerializeField] private float worldScale;
        private int diedsCount;
        private int round;

        public override void RegisterEvent()
        {
            base.RegisterEvent();
            Debug.Log($"[Game] controller registering channels [{name}]");
            worldScaleChannel.listener += GetWorldScale;
            enemyDiedChannel.eventChannel += EnemyDied;
        }
        public override void UnregisterEvent()
        {
            base.UnregisterEvent();
            worldScaleChannel.listener -= GetWorldScale;
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
            Debug.Log($"[Game][Start] controller start... round [{round}]");

            SpawnData spawnData;
            
            GameSetupSurvivor survivorSetup = (GameSetupSurvivor)setup;
            int size = survivorSetup.goals[round] / 2;

            for (int i = 0; i < size; i++)
            {
                spawnData = enemysSpawnsData[Random.Range(0, enemysSpawnsData.Length)];
                spawnData.InstantiateRandom(spawnPointChannel.Get(spawnData.type), round, Spawned);
            }
            for (int i = 0; i < survivorSetup.PlayerNumbers; i++)
            {
                spawnData = playersSpawnsData[Random.Range(0, playersSpawnsData.Length)];
                //This shuld be filled with the player status
                spawnData.InstantiateRandom(spawnPointChannel.Get(spawnData.type), player.level, Spawned);
            }
        }

        private void Spawned(GameObject spawned)
        {
            Debug.Log($"[Game][SpawnMode.None] Result {spawned.name} position: {spawned.transform.position}");

            Debug.Log($"[Game] spawned succes: [{spawned.name}]");

            IDestination[] idestination = spawned.GetComponentsInChildren<IDestination>();
            for (int i=0; i < idestination.Length; i++)
            {
                destinationChannel.eventChannel += idestination[i].SetDestination;
            }
            IAttack[] attacks = spawned.GetComponentsInChildren<IAttack>();
            for (int i = 0; i < attacks.Length; i++)
            {
                attackChannel.eventChannel += attacks[i].Attack;
            }
        }
        private float GetWorldScale()
        {
            Debug.Log($"[Game] Wrold Scale: {worldScale}");
            return worldScale;
        }
    }
}
