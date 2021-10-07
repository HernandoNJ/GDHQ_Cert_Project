using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    public static LaserPool sharedInstance;
    public List<GameObject> pooledLasers;
    public GameObject laserPrefab;
    public int lasersPoolAmount;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        pooledLasers = new List<GameObject>();
        GameObject tmpLaser;

        for (int i = 0; i < lasersPoolAmount; i++)
        {
            tmpLaser = Instantiate(laserPrefab);
            tmpLaser.SetActive(false);
            pooledLasers.Add(tmpLaser);
        }
    }

    public GameObject GetPooledLaser()
    {
        for (int i = 0; i < lasersPoolAmount; i++)
        {
            if (!pooledLasers[i].activeInHierarchy) return pooledLasers[i];
        }

        return null;
    }




}
