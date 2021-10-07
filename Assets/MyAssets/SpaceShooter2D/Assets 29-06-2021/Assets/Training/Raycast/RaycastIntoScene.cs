using System;
using UnityEngine;

public class RaycastIntoScene : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    
    // Useful for setting up values automatically
    private void OnValidate()
    {
        if (myCamera == null) myCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        Ray ray = myCamera.ScreenPointToRay(mouseScreenPosition);

        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.magenta);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
            raycastHit.collider.GetComponent<Renderer>().material.color = Color.cyan;
    }
}
