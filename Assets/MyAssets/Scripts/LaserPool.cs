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
            var tmpLaser = Instantiate(laserPrefab, lasersParent.transform, true);
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
