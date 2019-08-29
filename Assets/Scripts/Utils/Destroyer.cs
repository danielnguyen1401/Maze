using UnityEngine;

namespace Utils
{
    public class Destroyer : MonoBehaviour
    {
        public float timer;

        private void Start()
        {
            Destroy(gameObject, timer);
        }
    }
}