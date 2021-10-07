using System;
using UnityEngine;

namespace Training.Patterns.ObjectPooling
{
public class CubeSpawner : MonoBehaviour
{
    public CubePooler cubePooler;
    
    private void Start()
    {
        cubePooler = CubePooler._instance;
    }

    private void FixedUpdate()
    {
        cubePooler.SpawnGameObjectFromPool("Cubes",transform.position, Quaternion.identity);
        cubePooler.SpawnGameObjectFromPool("Capsules",transform.position, Quaternion.identity);
        cubePooler.SpawnGameObjectFromPool("Spheres",Vector3.up, Quaternion.identity);
    }
}
}
