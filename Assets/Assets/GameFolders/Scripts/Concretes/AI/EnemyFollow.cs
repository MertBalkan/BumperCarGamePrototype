using BumperCarGamePrototype.Concretes.Controllers;
using BumperCarGamePrototype.Concretes.Spawners;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BumperCarGamePrototype.Concretes.AI
{
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private int _myId;
        [SerializeField] private Transform[] _targets;
        [Header("---CURRENT TARGET---")]
        [SerializeField] private Transform _currentTarget;

        private NavMeshAgent _agent;
        private EnemySpawner _spawner;
        private float _currentTime;
        private float _changeTargetTime;
        
        public int MyId { get => _myId; set => _myId = value; }
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            _spawner = FindObjectOfType<EnemySpawner>();

            _targets = new Transform[FindObjectsOfType<EnemyController>().Length - 1 + FindObjectsOfType<CarController>().Length]; // -1 means: this gameobject cannot be targeted by himself
            _targets[_targets.Length - 1] = FindObjectOfType<CarController>().gameObject.transform; // Setting player to last target of enemy
            
            foreach (var spawnerId in _spawner.SpawnerIds)
            {
                if(this._myId == spawnerId)
                {
                    for (int i = 0; i < (_targets.Length - 1); i++)
                    {
                        _targets[i] = _spawner.SetMyTargetWithId(_myId, _targets);
                    }
                }
            }
        }
        private void Update()
        {

            _currentTime += Time.deltaTime;
            
            SetTarget();

            _changeTargetTime = Random.Range(100, 200);
            
            if (_currentTime >= _changeTargetTime) _currentTime = _changeTargetTime;
        }
        private void SetTarget()
        {
            int ranTarget = Random.Range(0, _targets.Length);
            _currentTarget = _targets[ranTarget];
            

            if (_currentTarget == null) return;

            if (_currentTime == _changeTargetTime)
            {
                _currentTime = 0.0f;
                _currentTarget = _targets[ranTarget];
                Debug.Log(this.gameObject.name + " hedef değiştirdi." + _currentTarget.name);
                _changeTargetTime = Random.Range(5, 10);
            }
            if (_currentTarget != null && _agent.enabled) _agent.SetDestination(_currentTarget.transform.position);
        }
    }
}