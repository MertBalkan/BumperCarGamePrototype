using BumperCarGamePrototype.Entites.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Controllers
{
    public class EnemyController : MonoBehaviour, IEntityController
    {
        private bool _isDisqualificated = false;

        public bool IsDisqualificated => _isDisqualificated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Corner"))
            {
                Debug.Log(this.gameObject.name + "Omg");
                _isDisqualificated = true;
            }
        }
    }
}