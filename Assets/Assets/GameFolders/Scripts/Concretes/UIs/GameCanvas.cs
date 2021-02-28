using BumperCarGamePrototype.Abstracts.Cameras;
using BumperCarGamePrototype.Abstracts.Combats;
using BumperCarGamePrototype.Concretes.Combats;
using BumperCarGamePrototype.Concretes.Controllers;
using BumperCarGamePrototype.Concretes.Managers;
using BumperCarGamePrototype.Concretes.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BumperCarGamePrototype.Concretes.UIs
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private Image[] _deathImages;
        [SerializeField] private List<GameObject> _deathEnemies;
        [SerializeField] private CarController _player;
        [SerializeField] private VictoryPanel _victoryPanel;
        [SerializeField] private Image _losePanel;
        [SerializeField] private AudioManager _audioManager;

        private EnemySpawner _enemySpawner;
        private ICameraShakeService _camera;

        public GameCanvas()
        {
            _deathEnemies = new List<GameObject>();
        }
        private void Awake()
        {
            _enemySpawner = FindObjectOfType<EnemySpawner>();
            _camera = new CameraController();
        }

        private void Update()
        {
            CheckEnableImages();
            ControlImageAndEnemy();
            if (_player.gameObject.GetComponent<IHealthService>().IsDead)
                PlayerLose();
            PlayerWin();
            ControlPlayerDeathOrDisqualificated();
        }

        private void CheckEnableImages()
        {

            foreach (var image in _deathImages)
            {
                foreach (var deathEnemy in _deathEnemies)
                {
                    if (image.gameObject.name == deathEnemy.gameObject.name)
                    {
                        image.gameObject.SetActive(true);
                    }
                }
            }
        }
        private void ControlImageAndEnemy()
        {
            foreach (var enemy in _enemySpawner.Enemies)
            {
                if (enemy.GetComponent<Health>().IsDead || enemy.GetComponent<EnemyController>().IsDisqualificated)
                {
                    if (!_deathEnemies.Contains(enemy))
                        _deathEnemies.Add(enemy);
                }
                if (enemy.GetComponent<EnemyController>().IsDisqualificated && enemy != null)
                {
                    enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                }
            }
        }
        private void ControlPlayerDeathOrDisqualificated()
        {
            if (_player.GetComponent<CarController>().IsDisqualificated && _player != null)
            {
                _player.gameObject.GetComponent<IHealthService>().IsDead = true;
                PlayerLose();
            }
        }
        private void PlayerLose()
        {
            if (_player.gameObject.GetComponent<IHealthService>().IsDead)
            {
                Time.timeScale = 0.0f;
                _audioManager.PlaySound("Lose");
                _losePanel.gameObject.SetActive(true);
            }
        }
        private void PlayerWin()
        {
            if (_deathEnemies.Count == _deathImages.Length)
            {
                ShowWinImage();
                _camera.IsCameraShake = false;
                _audioManager.PlaySound("Win");
            }
        }
        private void ShowWinImage()
        {
            _victoryPanel.ShowPanel(true);
            Time.timeScale = 0.0f;
        }
    }
}