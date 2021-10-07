using UnityEngine;

namespace Training.Patterns.ObjectPooling
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private PoolManager poolManager;

        private void Start()
        {
            poolManager = PoolManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = poolManager.GetBullet();
                bullet.transform.position = Vector3.zero;
            }
        }
    }
}