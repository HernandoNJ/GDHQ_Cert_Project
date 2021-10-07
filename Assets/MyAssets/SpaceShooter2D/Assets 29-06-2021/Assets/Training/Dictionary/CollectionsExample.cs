using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class CollectionsExample : MonoBehaviour
{
    public List<int> intList = new List<int>();

    private void Start()
    {
        intList.Add(5);
        intList.Add(7);
        intList.Add(11);

        intList.Remove(5);

        foreach (var num in intList)
        {
            Log("Number: " + num);
        }
    }

    private void Update()
    {
        // intList.Add(3);
        // intList.Add(7);
        // intList.Add(12);
    }
}
