using System;
using UnityEngine;

public class CubeLookingAtSphereWithLayers : MonoBehaviour
{
    private const float MAX_DISTANCE = 10f;

    [SerializeField] private Color raycastSingleTint = Color.green;
    [SerializeField] private Color raycastAllTint = Color.yellow;
    [SerializeField] private bool multiple;
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        if (multiple) RaycastMultiple();
        else RaycastSingle();
    }

    private void RaycastSingle()
    {
        Vector3 origin = transform.position;
        var direction = transform.forward;
        Debug.DrawRay(origin, direction * 10f, Color.red);
        Ray ray = new Ray(origin, direction);
        bool objectHit = Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance:10f, layerMask);
        if (objectHit)
            raycastHit.collider.GetComponent<Renderer>().material.color = raycastSingleTint;
    }
    private void RaycastMultiple()
    {
        Vector3 origin = transform.position;
        var direction = transform.forward;
        Debug.DrawRay(origin, direction * 5f, Color.yellow);
        Ray ray = new Ray(origin, direction);
        var multipleHits = Physics.RaycastAll(ray, MAX_DISTANCE, layerMask);
        foreach (var raycastHit in multipleHits)
        {
            raycastHit.collider.GetComponent<Renderer>().material.color = raycastAllTint;
        }
    }
}
