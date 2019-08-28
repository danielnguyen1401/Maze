using Manager;
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
            gameObject.SetActive(false);
        }

        private void HitEnemy()
        {
            GameUiManager.Instance.enemyGetScore.Invoke(10);
            if (Debug.isDebugBuild) Debug.LogWarning("hit enemy");
            gameObject.SetActive(false);
        }
    }
}