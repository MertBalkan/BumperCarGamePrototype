using BumperCarGamePrototype.Concretes.Managers;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.UIs
{
    public class LosePanel : MonoBehaviour
    {
        private void Update()
        {
            if (!this.gameObject.activeInHierarchy) return;
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