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
        private Modes _cacheMode;

        public Camera ThirdPersonCam
        {
            set { _thirdPersonCam = value; }
        }

        private void Start()
        {
            _cacheMode = cameraModes;
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

        private void ActiveOnlyOneCame()
        {
            if (cameraModes == Modes.TopDown)
            {
                PlayerController.Instance.Cam = topDownCam;
                if (LookAtCamera.Instance != null)
                    LookAtCamera.Instance.Target = topDownCam.transform;
                topDownCam.gameObject.SetActive(true);
                _thirdPersonCam.gameObject.SetActive(false);
            }
            else
            {
                PlayerController.Instance.Cam = _thirdPersonCam;
                if (LookAtCamera.Instance != null)
                    LookAtCamera.Instance.Target = _thirdPersonCam.transform;
//                topDownCam.gameObject.SetActive(false);
                _thirdPersonCam.gameObject.SetActive(true);
            }
        }

        public void Cache()
        {
            _cacheMode = cameraModes;
            cameraModes = Modes.TopDown;
            ActiveOnlyOneCame();
        }

        public void LoadCache()
        {
            cameraModes = _cacheMode;
            ActiveOnlyOneCame();
        }
    }
}