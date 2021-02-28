using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Controllers
{
    public class SpectatorController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        void Update()
        {
            float sin = Mathf.Sin(Time.time);
            Vector3 moveVec = new Vector3(sin * _moveSpeed, transform.localPosition.y, transform.localPosition.z);
            this.transform.localPosition = moveVec;
        }
    }
}