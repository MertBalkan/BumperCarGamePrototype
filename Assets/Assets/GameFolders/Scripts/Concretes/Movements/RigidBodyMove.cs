using BumperCarGamePrototype.Abstracts.Inputs;
using BumperCarGamePrototype.Abstracts.Movements;
using BumperCarGamePrototype.Entites.Controllers;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Movements
{
    public class RigidBodyMove : IMoveService
    {
        private IEntityController _controller;
        private float _speed;

        private Vector3 _move = Vector3.zero;

        public RigidBodyMove(IEntityController controller, float speed)
        {
            _speed = speed;
            _controller = controller;
        }

        public void MoveAction(float dir)
        {
            _move = new Vector3(0, 0, dir) * Time.fixedDeltaTime * _speed;
            _controller.transform.GetComponent<Rigidbody>().MovePosition(_controller.transform.position
                                                                        + _controller.transform.TransformDirection(_move));
        }
    }
}