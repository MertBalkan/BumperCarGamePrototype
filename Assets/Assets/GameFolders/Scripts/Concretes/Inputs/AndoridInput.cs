using BumperCarGamePrototype.Abstracts.Inputs;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Inputs
{
    public class AndoridInput : IInputService
    {
        public float Vertical => SimpleInput.GetAxis("Vertical");

        public float Horizontal => SimpleInput.GetAxis("Horizontal");
    }
}