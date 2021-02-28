using UnityEngine;
using TMPro;
using BumperCarGamePrototype.Concretes.Managers;

namespace BumperCarGamePrototype.Concretes.UIs
{
    public class CoinCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Update()
        {
            ShowScore();
        }
        public void ShowScore()
        {
            _scoreText.text = "Coins:" + GameManager.Instance.Score.ToString();
        }
    }
}