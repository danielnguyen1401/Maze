using Enemy;
using Manager;
using Player;
using UnityEngine;

namespace TargetController
{
    public class TriggerEndPoint : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HitPlayer();
            }

            if (other.CompareTag("Enemy"))
            {
                HitEnemy();
            }
        }

        private void HitPlayer()
        {
            GameUiManager.Instance.playerGetScore.Invoke(10);
            GameUiManager.Instance.ShowFinishLevel();
            gameObject.SetActive(false);
            GameManager.Instance.GameEnded = true;
            EnemyController.Instance.CancelDestination();
        }

        private void HitEnemy()
        {
            if (Debug.isDebugBuild) Debug.LogWarning("hit enemy");
            GameUiManager.Instance.enemyGetScore.Invoke(10);
            GameUiManager.Instance.ShowFinishLevel();
            gameObject.SetActive(false);
            GameManager.Instance.GameEnded = true;
            PlayerController.Instance.CancelDestination();
        }
    }
}