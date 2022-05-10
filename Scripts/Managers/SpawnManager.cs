using System.Linq;
using UnityEngine;

namespace ArChi
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private SpawnPointChannel spawnPointChannel;
        [SerializeField] private SpawnPoint[] spawnPoints;

        private void OnEnable()
        {
            spawnPointChannel.listener += GetSpawnPoint;
        }
        private void OnDisable()
        {
            spawnPointChannel.listener -= GetSpawnPoint;
        }

        public Transform GetSpawnPoint(SpawnType type)
        {
            Debug.Log($"[Game] spawn point type: {type}");
            var points = spawnPoints.Where(x => x.type == type);
            return points.ElementAt(Random.Range(0, points.Count())).point;
        }
    }
}