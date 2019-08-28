using Enemy;
using Manager;
using Player;
using UnityEngine;

namespace TargetController
{
    public class TriggerEndPoint : MonoBehaviour
    {
        private readonly string _playerTag = "Player";
        private readonly string _enemyTag = "Enemy";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
                HitPlayer();
            if (other.CompareTag(_enemyTag))
                HitEnemy();
        }

        private void HitPlayer()
        {
            GameUiManager.Instance.playerGetScore.Invoke(10);
            gameObject.SetActive(false);
            GameManager.Instance.GameEnded = true;
            EnemyController.Instance.CancelDestination();
            GameUiManager.Instance.ShowFinishLevel(true);
        }

        private void HitEnemy()
        {
            GameUiManager.Instance.enemyGetScore.Invoke(10);
            gameObject.SetActive(false);
            GameManager.Instance.GameEnded = true;
            PlayerController.Instance.CancelDestination();
            GameUiManager.Instance.ShowFinishLevel(false);
        }
    }
}