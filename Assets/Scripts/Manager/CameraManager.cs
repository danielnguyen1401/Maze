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
                if (PlayerController.Instance.Cam == null)
                    PlayerController.Instance.Cam = topDownCam;
                topDownCam.gameObject.SetActive(true);
                _thirdPersonCam.gameObject.SetActive(false);
            }
            else
            {
                if (PlayerController.Instance.Cam == null)
                    PlayerController.Instance.Cam = _thirdPersonCam;
                topDownCam.gameObject.SetActive(false);
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