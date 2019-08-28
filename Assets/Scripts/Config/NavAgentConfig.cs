namespace Config
{
    public class NavAgentConfig : SingletonMono<NavAgentConfig>
    {
        public float speed;
        public float angularSpeed;
        public float acceleration;
        public float obstacleAvoidRadius;
    }
}
