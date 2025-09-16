using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace InGameLogics
{
    public class InGameScene : MonoBehaviour
    {
        [SerializeField] private Transform monsterSpawner;
        [SerializeField] private Transform spawnLine1;
        [SerializeField] private Transform spawnLine2;

        private Dictionary<int, Monster> _monsters = new ();
        private Dictionary<int, Pawn> _players = new ();

        public IReadOnlyDictionary<int, Monster> Monsters => _monsters;
        public IReadOnlyDictionary<int, Pawn> Players => _players;

        public static InGameScene Instance { get; private set; }

        [SerializeField] private Monster monsterPrefab;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        float spawnedTime = 0f;
        private void Update()
        {
            spawnedTime += Time.deltaTime;
            if (spawnedTime > 0.3f)
            {
                spawnedTime = 0f;
                SpawnMonster(0);
            }
        }

        public void SpawnMonster(int monsterId)
        {
            Vector3 spawnPoint = Vector3.Lerp(spawnLine1.position, spawnLine2.position, Random.value);

            Monster monster = GameObject.Instantiate(monsterPrefab, spawnPoint, Quaternion.identity, transform);
            int instanceId = CreateMonsterId();
            monster.Init(monsterId, instanceId);
            _monsters.Add(instanceId, monster);
        }

        static int monsterInstanceId = 0;
        public static int CreateMonsterId()
        {
            return monsterInstanceId++;
        }
        public void RemoveMonster(int instanceId)
        {
            if (_monsters.TryGetValue(instanceId, out var monster))
            {
                _monsters.Remove(instanceId);
                Destroy(monster.gameObject);
            }
        }

    }
}
