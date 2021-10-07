using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test22 : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))  Debug.Log("right trigger"); 
    }
}
