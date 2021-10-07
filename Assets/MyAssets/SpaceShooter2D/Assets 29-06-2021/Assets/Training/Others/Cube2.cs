using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube2 : MonoBehaviour
{

    public Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 100f);
    }

    // private void FixedUpdate()
    // {
    //     gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
    // }


}
