using UnityEngine;

namespace Cam
{
    public class LookAtCamera : SingletonMono<LookAtCamera>
    {
        private Transform _target;

        public Transform Target
        {
            set { _target = value; }
        }

        private void Update()
        {
            transform.LookAt(_target);
        }
    }
}