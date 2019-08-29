using Cam;
using Player;
using UnityEngine;
using Utils;

namespace Manager
{
    public enum Modes
    {
        ThirdPerson,
        TopDown
    };

    public class CameraManager : SingletonMono<CameraManager>
    {
        private bool _isChanged;
        public Modes cameraModes;
        public Camera topDownCam;
        private Camera _thirdPersonCam;

        public Camera ThirdPersonCam
        {
            set { _thirdPersonCam = value; }
        }

        private void Start()
        {
            cameraModes = Modes.TopDown;
        }

        private void Update()
        {
            if (!GameManager.Instance.GameStarted || GameManager.Instance.GameEnded) return;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!_isChanged)
                {
                    ChangeModeCamera();
                    _isChanged = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.Return))
                _isChanged = false;
        }

        private void ChangeModeCamera()
        {
            cameraModes = cameraModes.Next<Modes>();
            ActiveOnlyOneCame();
        }

        public void SetCameraToTopDown()
        {
            cameraModes = Modes.TopDown;
            _thirdPersonCam.enabled = false;
            topDownCam.enabled = true;
        }

        public void ActiveOnlyOneCame()
        {
            _thirdPersonCam.enabled = false;
            topDownCam.enabled = false;
            if (cameraModes == Modes.TopDown)
            {
                PlayerController.Instance.Cam = topDownCam;
                if (LookAtCamera.Instance != null)
                    LookAtCamera.Instance.Target = topDownCam.transform;
                topDownCam.enabled = true;
            }
            else
            {
                PlayerController.Instance.Cam = _thirdPersonCam;
                if (LookAtCamera.Instance != null)
                    LookAtCamera.Instance.Target = _thirdPersonCam.transform;
                _thirdPersonCam.enabled = true;
            }
        }
    }
}