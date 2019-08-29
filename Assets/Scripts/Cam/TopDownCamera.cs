using Manager;
using UnityEngine;

namespace Cam
{
    public class TopDownCamera : SingletonMono<TopDownCamera>
    {
        public Camera cam;
        private GameObject _player; 
        public Vector3 offset;
        private Vector3 _velocity;
        [SerializeField] private float followSpeed;

        private void Start()
        {
        }

        public void SetPlayerForCam(GameObject playerObj)
        {
            _player = playerObj;
        }

        private void TopDownView()
        {
            if (_player != null)
            {
                var targetPosition = _player.transform.position + offset;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, followSpeed);
            }
        }

        private void Update()
        {
            TopDownView();
        }
    }
}