using Manager;
using UnityEngine;

namespace Cam
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        public Camera cam;
        public GameObject player;
        private Vector3 _velocity;
        public float followSpeed = 1.5f;

        private void Start()
        {
            CameraManager.Instance.ThirdPersonCam = cam;
        }

        private void Update()
        {
//            ThirdPersonView();
        }

        private void ThirdPersonView()
        {
            var targetPosition = player.transform.forward;
            transform.forward = Vector3.SmoothDamp(transform.forward, targetPosition, ref _velocity, followSpeed);
        }
    }
}