using BumperCarGamePrototype.Abstracts.Combats;
using BumperCarGamePrototype.Concretes.Controllers;
using BumperCarGamePrototype.Entites.Controllers;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Combats
{
    public class HitCombat : MonoBehaviour, IHitService
    {
        private IEntityController _player;
        private IEntityController _enemy;

        private bool _isHit;

        public bool IsHit { get => _isHit; set => _isHit = value; }

        private void Awake()
        {
            _player = GetComponent<CarController>();
            _enemy = GetComponent<EnemyController>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                _player = GetComponent<IEntityController>();
                _player.transform.GetComponent<IHealthService>().CurrentHealth -= 10;
                IsHit = true;
            }

            if (collision.gameObject.tag == "Player")
            {
                _enemy = GetComponent<IEntityController>();
                _enemy.transform.GetComponent<IHealthService>().CurrentHealth -= 10;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                _player = GetComponent<IEntityController>();
                Debug.Log(this.gameObject.name + "/Combat/" + _player.transform.gameObject.name);
                if (_player.transform.GetComponent<CarController>() == null) return;
                _player.transform.GetComponent<CarController>().CurrentSoundTime = 0;
            }
            if (collision.gameObject.tag == "Player")
            {
                _enemy = GetComponent<IEntityController>();
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                IsHit = false;
            }
        }
    }
}