using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    public static LaserPool sharedInstance;
    public List<GameObject> pooledLasers;
    public GameObject laserPrefab;
    public GameObject lasersParent;    
    public int lasersPoolAmount;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        pooledLasers = new List<GameObject>();
        SetNewLasers();
    }

    private void SetNewLasers()
    {
        for (int i = 0; i < lasersPoolAmount; i++)
        {
            var tempLaser = Instantiate(laserPrefab, lasersParent.transform, true);
            tempLaser.SetActive(false);
            pooledLasers.Add(tempLaser);
        }
    }

    public GameObject GetPooledLaser()
    {
        for (int i = 0; i < lasersPoolAmount; i++)
        {
            if (!pooledLasers[i].activeInHierarchy) return pooledLasers[i];
            if(pooledLasers[i] == null) Debug.LogWarning("Laser pool is null");
        }
        
        return null;
    }
}
