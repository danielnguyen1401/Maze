﻿using Config;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        private bool _setDestination;

        private void Start()
        {
            SetupAgent();
        }

        private void SetupAgent()
        {
            agent.speed = NavAgentConfig.Instance.speed;
            agent.angularSpeed = NavAgentConfig.Instance.angularSpeed;
            agent.acceleration = NavAgentConfig.Instance.acceleration;
            agent.radius = NavAgentConfig.Instance.obstacleAvoidRadius;
        }

        private void Update()
        {
            if (!GameManager.Instance.GameStarted) return;
            if (!_setDestination)
            {
                agent.destination = GameManager.Instance.targetPoint.position;
                _setDestination = true;
            }
        }
    }
}