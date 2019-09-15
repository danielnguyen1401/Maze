using Manager;

namespace Enemy
{
    public class EnemyController : SingletonMono<EnemyController>
    {
        private bool _setDestination;
        private Pathfinding Pathfinding;

        private void Start()
        {
            Pathfinding = Pathfinding.Instance;
            Pathfinding.StartPosition = transform;
        }

        private void SetupAgent()
        {
//            agent.speed = 0;
//            agent.angularSpeed = NavAgentConfig.Instance.angularSpeed;
//            agent.acceleration = NavAgentConfig.Instance.acceleration;
//            agent.radius = NavAgentConfig.Instance.obstacleAvoidRadius;
//            agent.enabled = false;
        }

        public void EnableAgent()
        {
//            agent.enabled = true;
//            agent.speed = NavAgentConfig.Instance.speed;
        }

        private void Update()
        {
//            if (!GameManager.Instance.GameStarted || GameManager.Instance.GameEnded) return;
//            if (!_setDestination)
//            {
//                agent.destination = GameManager.Instance.targetPoint.position;
//                _setDestination = true;
//            }
        }

        public void CancelDestination()
        {
//            if (agent.gameObject.activeInHierarchy)
//                agent.isStopped = true;
            Pathfinding.StopMoveEnemy();
        }
    }
}