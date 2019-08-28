using Camera;
using Config;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerController : SingletonMono<PlayerController>
    {
        [SerializeField] NavMeshAgent agent;
        [SerializeField] private ParticleSystem cursorParticle;
        private UnityEngine.Camera _camera;
        private const float Multiple = 1.2f;
        private ParticleSystem _cursorFx;

        private void Start()
        {
            _cursorFx = Instantiate(cursorParticle);
            _camera = CameraController.Instance.mainCamera;
            SetupNavAgent();
        }

        private void SetupNavAgent()
        {
            agent.speed = 0;
            agent.angularSpeed = NavAgentConfig.Instance.angularSpeed * Multiple;
            agent.acceleration = NavAgentConfig.Instance.acceleration * Multiple;
            agent.radius = NavAgentConfig.Instance.obstacleAvoidRadius * Multiple;
            agent.enabled = false;
        }

        public void EnableAgent()
        {
            agent.enabled = true;
            agent.speed = NavAgentConfig.Instance.speed * Multiple;
        }

        private void Update()
        {
            if (!GameManager.Instance.GameStarted || GameManager.Instance.GameEnded)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 50))
                {
                    CursorEffect(hit.point);
                    agent.destination = hit.point;
                }
            }
        }

        private void CursorEffect(Vector3 hitPoint)
        {
            _cursorFx.Stop();
            _cursorFx.transform.position = hitPoint + new Vector3(0, 1, 0);
            _cursorFx.Play();
        }

        public void CancelDestination()
        {
            agent.isStopped = true;
        }
    }
}