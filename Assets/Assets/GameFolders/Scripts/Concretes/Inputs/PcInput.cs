using BumperCarGamePrototype.Abstracts.Inputs;
using UnityEngine;

namespace BumperCarGamePrototype.Concretes.Inputs
{
    public class PcInput : IInputService
    {
        public float Vertical => Input.GetAxis("Vertical");
        public float Horizontal => Input.GetAxis("Horizontal");

    }
}
