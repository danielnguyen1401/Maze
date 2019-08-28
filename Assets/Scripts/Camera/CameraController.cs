using System;
using Manager;
using UnityEngine;
using Utils;

namespace Camera
{
    public enum Modes
    {
        PlayerView,
        SceneView
    };

    public class CameraController : SingletonMono<CameraController>
    {
        public Modes cameraModes;
        public UnityEngine.Camera mainCamera;
        private bool _isChanged;
        public GameObject player; //Public variable to store a reference to the player game object
        private Vector3 _offset;

        private void Start()
        {
        }

        private void Setup()
        {
            switch (cameraModes)
            {
                case Modes.SceneView:
                    CameraToScene();
                    break;
                case Modes.PlayerView:
                    CameraToPlayer();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CameraToScene()
        {
            if (player != null)
            {
                _offset = transform.position - player.transform.position;
                transform.position = player.transform.position + _offset;
            }
        }

        private void CameraToPlayer()
        {
            if (player != null)
            {
                Debug.LogWarning("look at player");
                _offset = transform.position - player.transform.position;
                transform.position = player.transform.position + _offset;
            }
        }

        private void ChangeModeCamera()
        {
            cameraModes = cameraModes.Next<Modes>();
            Debug.LogWarning(cameraModes.ToString());
        }

        private void Update()
        {
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

            if (!GameManager.Instance.GameStarted) return;
            Setup();
        }
    }
}