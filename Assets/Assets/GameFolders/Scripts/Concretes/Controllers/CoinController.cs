using BumperCarGamePrototype.Concretes.Managers;
using System;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Controllers
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Transform _coins;
        
        private void Update()
        {
            CoinMove();
        }
        private void CoinMove()
        {
            transform.Rotate(new Vector3(0, _moveSpeed * Time.deltaTime, 0));
        }
    }
}