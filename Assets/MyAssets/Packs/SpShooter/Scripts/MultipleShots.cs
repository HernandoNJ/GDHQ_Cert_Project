using System.Collections;
using UnityEngine;

namespace MyAssets.Packs.SpShooter.Scripts
{
public class MultipleShots : MonoBehaviour
{
    [SerializeField] private int shotsAmount;
    [SerializeField] private int timesToShoot;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject shootingPoint;

    void Start()
    {
        shootingPoint = GameObject.Find("FirePoint");
        shotsAmount = 30;
        timesToShoot = 7;
        StartCoroutine(SpreadShotsRoutine());
    }

    IEnumerator SpreadShotsRoutine()
    {
        while (timesToShoot > 0)
        {
            for (int i = 1; i <= timesToShoot; i++)
            {
                SpreadMultipleShots();
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    private void SpreadMultipleShots()
    {
        for (int i = 1; i < shotsAmount; i++)
        {
            int n = i * 30;
            if (n <= 360)
            {
                Quaternion shotRotation = Quaternion.Euler(0, 0, n);
                Instantiate(shotPrefab, shootingPoint.transform.position, shotRotation);
                Destroy(gameObject, 2f);
                //Destroy(shotPrefab, 2f);  *** Destroying assets is not permitted to avoid data loss.
            }
        }
    }
}
}
