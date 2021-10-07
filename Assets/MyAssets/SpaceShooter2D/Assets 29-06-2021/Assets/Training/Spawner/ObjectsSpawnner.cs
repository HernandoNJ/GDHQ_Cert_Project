using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class ObjectsSpawnner : MonoBehaviour
{
    public GameObject[] sourceObjects;
    public List<GameObject> objectsCreated = new List<GameObject>();
    public int objCounter { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (objCounter == 10)
                ChangeColor();
            else
                Spawner();
        }
    }

    public void Spawner()
    {
        objCounter++;

        int randObj = Random.Range(0, sourceObjects.Length);
        GameObject obj = sourceObjects[randObj]; // WeaponLauncher referencing a prefab

        float randX = Random.Range(-10, 10);
        float randY = Random.Range(-10, 10);
        Vector3 objPos = new Vector3(randX, randY, 0);

        // ASK difference between prefab and WeaponLauncher referencing
        // Adding obj to List - throws an error when trying to change color
        // Instantiate(obj, objPos, Quaternion.identity);
        // objectsCreated.Add(obj);

        GameObject go = Instantiate(obj, objPos, Quaternion.identity);
        objectsCreated.Add(go);
    }

    public void ChangeColor()
    {
        foreach (var go in objectsCreated)
            go.GetComponent<MeshRenderer>().material.color = Color.green;

        objectsCreated.Clear();
        Log("objCounter = " + objCounter);
        return;
    }
}



