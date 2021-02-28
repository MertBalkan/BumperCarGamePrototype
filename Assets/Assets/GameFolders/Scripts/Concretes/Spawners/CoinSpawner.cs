using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Spawners
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _coin;
        [SerializeField] private Transform _coins;
        [SerializeField] private float _spawnRadius = 15f;
        [SerializeField] private float _spawnTime = 1f;
        [SerializeField] private float _spawnRepeatRate = 2f;
        [SerializeField] private float _spawnRange = 5.0f;

        private float _spawnAmountMin = default;
        private float _spawnAmountMax = default;
            
        private void Start()
        {
            InvokeRepeating("SpawnCoin", _spawnTime, _spawnRepeatRate);
        }
        private void SpawnCoin()
        {
            _spawnAmountMin = -(_spawnRadius) + _spawnRange;
            _spawnAmountMax = _spawnRadius - _spawnRange;

            GameObject coinObj = Instantiate(_coin, 
                transform.position + new Vector3(Random.Range(_spawnAmountMin, _spawnAmountMax), 0, Random.Range(_spawnAmountMin, _spawnAmountMax)), 
                transform.rotation) as GameObject;

            coinObj.transform.parent = _coins.transform;
        }
        private void OnDrawGizmos()
        {
            OnDrawGizmosSelected();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    }
}