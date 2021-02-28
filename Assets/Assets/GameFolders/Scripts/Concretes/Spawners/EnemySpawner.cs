using BumperCarGamePrototype.Concretes.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private List<int> _spawnerIds;
        [SerializeField] private GameObject _enemy;

        private int _enemyCount;
        private List<GameObject> _enemies;

        public List<Transform> SpawnPoints { get => _spawnPoints; set => _spawnPoints = value; }
        public List<int> SpawnerIds { get => _spawnerIds; set => _spawnerIds = value; }
        public int EnemyCount { get => _enemyCount; set => _enemyCount = value; }
        public List<GameObject> Enemies { get => _enemies; set => _enemies = value; }

        public EnemySpawner()
        {
            SpawnPoints = new List<Transform>();
            Enemies = new List<GameObject>();

            SpawnerIds = new List<int>();
        }

        private void Start()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (SpawnPoints.Count == 0 || SpawnPoints == null) return;

            foreach (var spawnPoint in SpawnPoints)
            {
                EnemyCount++;
                GameObject enemy = Instantiate(_enemy, spawnPoint.transform.localPosition, spawnPoint.transform.localRotation) as GameObject;
                Enemies.Add(enemy);
                
                enemy.gameObject.name = "Enemy" + EnemyCount;

               // Debug.Log("SpawnPoint:" + _spawnPoints[2].gameObject.name);
              ///  Debug.Log("SpawnPoint:" + _spawnerIds[0].ToString());
              ///  
                if(enemy.gameObject.name == "Enemy" + _spawnerIds[0].ToString())
                {
                    Debug.Log(enemy.gameObject.name);
                }
                if (enemy.gameObject.name == "Enemy" + _spawnerIds[1].ToString())
                {
                    Debug.Log(enemy.gameObject.name);
                }
                if (enemy.gameObject.name == "Enemy" + _spawnerIds[2].ToString())
                {
                    Debug.Log(enemy.gameObject.name);
                }
                CheckForIds(enemy);                
            }
        }
        //Soory for this code. I write here spagetti style. Because of my time was not enough
        public Transform SetMyTargetWithId(int myId, Transform[] _myTargets)
        {
            if(myId == 1)
            {
                _myTargets[0] = Enemies[1].gameObject.transform;
                _myTargets[1] = Enemies[2].gameObject.transform;
            }
            if (myId == 2)
            {
                _myTargets[0] = Enemies[2].gameObject.transform;
                _myTargets[1] = Enemies[1].gameObject.transform;
            }
            if (myId == 3)
            {
                _myTargets[0] = Enemies[0].gameObject.transform;
                _myTargets[1] = Enemies[1].gameObject.transform;
            }
            return null;
        }
        private void CheckForIds(GameObject enemy)
        {
            for (int i = 0; i < SpawnerIds.Count; i++)
            {
                if (SpawnerIds[i].ToString() == EnemyCount.ToString())
                {
                    enemy.gameObject.GetComponent<EnemyFollow>().MyId = EnemyCount;
                }
            }
        }
    }
}