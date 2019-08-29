using Cam;
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
        private Camera _cam;
        private const float Multiple = 1.2f;
        private ParticleSystem _cursorFx;
        public ThirdPersonCamera thirdPersonCamera;
        [SerializeField] private LayerMask ground;

        public Camera Cam
        {
            set { _cam = value; }
        }

        private void Start()
        {
            _cursorFx = Instantiate(cursorParticle);
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
            if (!GameManager.Instance.GameStarted || GameManager.Instance.GameEnded) return;
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, 100, ground))
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