using BumperCarGamePrototype.Concretes.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.UIs
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _credits;

        public void PlayGame()
        {
            GameManager.Instance.StartGame(1);
        }
        public void ShowCredits()
        {
            _credits.SetActive(true);    
        }
        public void QuitGame()
        {
            GameManager.Instance.QuitGame();
        }
    }
}