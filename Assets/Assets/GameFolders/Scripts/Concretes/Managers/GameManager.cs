using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BumperCarGamePrototype.Concretes.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int _score;

        public static GameManager Instance { get; private set; }
        public int Score { get => _score; set => _score = value; }

        public System.Action<int> OnHealthChange;
        public System.Action<int> OnScoreChange;

        private void Awake()
        {
            SingletonMethod();
        }
        private void Update()
        {
            SetTimeTo1();
        }
        public void StartGame(int buildIndex)
        {
            StartCoroutine(StartGameAsync(buildIndex));
            _score = 0;
        }
        public void LoadScene(int buildIndex)
        {
            _score = 0;
            StartCoroutine(LoadSceneAsync(buildIndex));
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        public void SetTimeTo1()
        {
            Time.timeScale = 1.0f;
        }
        private IEnumerator LoadSceneAsync(int buildIndex)
        {
            SceneManager.LoadSceneAsync(buildIndex);
            yield return null;
        }
        private IEnumerator StartGameAsync(int buildIndex)
        {
            SceneManager.LoadSceneAsync(buildIndex);
            yield return null;
        }
        public void TakeScore(int score)
        {
            _score = score;
            OnScoreChange?.Invoke(_score);
        }
        private void SingletonMethod()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}