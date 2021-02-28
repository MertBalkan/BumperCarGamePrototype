using BumperCarGamePrototype.Concretes.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BumperCarGamePrototype.Concretes.UIs
{
    public class VictoryPanel : MonoBehaviour
    {
        public void ShowPanel(bool isActive)
        {
            this.gameObject.SetActive(isActive);
        }
        public void ReturnMenuButtonDown()
        {
            GameManager.Instance.LoadScene(0);
        }
        public void PlayAgainButtonDown()
        {
            GameManager.Instance.LoadScene(1);
        }
    }
}