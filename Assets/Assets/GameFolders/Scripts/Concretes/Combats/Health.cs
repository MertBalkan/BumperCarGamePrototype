using BumperCarGamePrototype.Abstracts.Combats;
using UnityEngine;
using UnityEngine.UI;

namespace BumperCarGamePrototype.Concretes.Combats
{
    public class Health : MonoBehaviour, IHealthService
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _currentHealth;
        [SerializeField] private Slider _healthSlider;

        private bool _isDead = false;

        public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
        public bool IsDead { get => _isDead; set => _isDead = value; }

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
            _healthSlider.value = CurrentHealth;
        }
        private void OnDisable()
        {
            _healthSlider.value = 0.0f;
        }
        private void Update()
        {
            _healthSlider.value = CurrentHealth;
            CheckIsHealthZero();
        }
        private void CheckIsHealthZero()
        {
            if (CurrentHealth <= 0)
            {
                if (this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
                {
                    this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                }
                _isDead = true;
                _healthSlider.value = 0.0f;
                CurrentHealth = 0;
            }
        }
    }
}