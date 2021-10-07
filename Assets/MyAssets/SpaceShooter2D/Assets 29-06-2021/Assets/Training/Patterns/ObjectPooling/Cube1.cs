using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Training.Patterns.ObjectPooling
{
public class Cube1 : MonoBehaviour, IPooledObject
{
   public float upForce = 1f;
   public float sideForce = 0.1f;

   public void OnObjectSpawn()
   {
      float xForce = Random.Range(-sideForce, sideForce);
      float yForce = Random.Range(-upForce / 2f, upForce);
      float zForce = Random.Range(-sideForce, sideForce);

      Vector3 force = new Vector3(xForce, yForce, zForce);
      GetComponent<Rigidbody>().velocity = force;
   }
}
}
