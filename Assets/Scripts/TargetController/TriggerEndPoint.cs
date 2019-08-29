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
                HitPlayer(other.gameObject);
            if (other.CompareTag(_enemyTag))
                HitEnemy(other.gameObject);
        }

        private void HitPlayer(GameObject obj)
        {
            GameUiManager.Instance.playerGetScore.Invoke(10);
            gameObject.SetActive(false);
            GameManager.Instance.GameEnded = true;
            GameManager.Instance.PlayWinLoseEffect(obj.transform.position);
            CancelAgent();
            GameUiManager.Instance.ShowFinishLevel(true);
            CameraManager.Instance.SetCameraToTopDown();
        }

        private void HitEnemy(GameObject obj)
        {
            GameUiManager.Instance.enemyGetScore.Invoke(10);
            gameObject.SetActive(false);
            GameManager.Instance.GameEnded = true;
            GameManager.Instance.PlayWinLoseEffect(obj.transform.position);
            CancelAgent();
            GameUiManager.Instance.ShowFinishLevel(false);
            CameraManager.Instance.SetCameraToTopDown();
        }

        private void CancelAgent()
        {
            PlayerController.Instance.CancelDestination();
            EnemyController.Instance.CancelDestination();
        }
    }
}