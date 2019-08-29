using System;
using Manager;
using UnityEngine;
using Utils;

namespace Camera
{
    public enum Modes
    {
        ThirdPerson,
        TopDown
    };

    public class CameraController : SingletonMono<CameraController>
    {
        public Modes cameraModes;
        public UnityEngine.Camera mainCamera;
        private bool _isChanged;
        [HideInInspector] public GameObject player; //Public variable to store a reference to the player game object
        public Vector3 offset;
        private Vector3 _velocity;
        [SerializeField] private float followSpeed;

        private void Start()
        {
//            Debug.LogWarning("cameraModes " + cameraModes);
        }

        public void SetupOffset(GameObject playerObj)
        {
            player = playerObj;
        }

        private void Setup()
        {
            switch (cameraModes)
            {
                case Modes.TopDown:
                    TopDownView();
                    break;
                case Modes.ThirdPerson:
                    ThirdPersonView();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TopDownView()
        {
            if (player != null)
            {
                var targetPosition = player.transform.position + offset;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, followSpeed);
            }
        }

        private void ThirdPersonView()
        {
            if (player != null)
            {
                Debug.LogWarning("look at player");
                offset = transform.position - player.transform.position;
                transform.position = player.transform.position + offset;
            }
        }

        private void ChangeModeCamera()
        {
            cameraModes = cameraModes.Next<Modes>();
            Debug.LogWarning(cameraModes.ToString());
        }

        private void Update()
        {
//            if (!GameManager.Instance.GameStarted || GameManager.Instance.GameEnded) return;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!_isChanged)
                {
                    ChangeModeCamera();
                    _isChanged = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.Return))
            {
                _isChanged = false;
            }

            Setup();
        }
    }
}