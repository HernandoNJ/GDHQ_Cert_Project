using System.Collections.Generic;
using UnityEngine;

namespace Training.Patterns.ObjectPooling
{
    public class PoolManager : SingletonBP<PoolManager>
    {
        [SerializeField] private GameObject bulletContainer;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private List<GameObject> bulletPool;

        private void Start()
        {
            BulletsGenerator(10);
        }

        private List<GameObject> BulletsGenerator(int bulletsAmount)
        {
            for (int i = 0; i < bulletsAmount; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletContainer.transform, true);
                //bullet.SetActive(false);
                bulletPool.Add(bullet);

                // WeaponLauncher bullet = Instantiate(bulletPrefab);
                // bullet.transform.parent = bulletContainer.transform;
                // //bullet.SetActive(false);
                // bulletPool.Add(bullet);
            }
            return bulletPool;
        }

        public GameObject GetBullet()
        {
            // if bullet is not active, activate it and return it to player
            // if false, generate 1 more bullet and give it to player
            foreach (var bullet in bulletPool)
            {
                if (bullet.activeInHierarchy == false)
                {
                    bullet.SetActive(true);
                    return bullet;
                }
            }

            GameObject newBullet = Instantiate(bulletPrefab, bulletContainer.transform, true);
            bulletPool.Add(newBullet);
            return newBullet;
        }
    }
}