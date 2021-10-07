using UnityEngine;

public class CubeLookingAtSphere : MonoBehaviour
{
    [SerializeField] private Color singleColor = Color.green;
    [SerializeField] private Color allColor = Color.yellow;
    [SerializeField] private bool isMultiple;
    [SerializeField] private float speed;

    private void Update(){ RaycastSingle(); RaycastMultiple(); }
    private void RaycastSingle()
    {
        Vector3 origin = transform.position;
        var direction = transform.forward;
        Debug.DrawRay(origin, direction * 5f, Color.red);
        bool objectHit = Physics.Raycast(new Ray(origin, direction), out RaycastHit hitInfo);
        if (objectHit) hitInfo.collider.GetComponent<Renderer>().material.color = singleColor;
    }
    private void RaycastMultiple()
    {
        Vector3 origin = transform.position;
        var direction = transform.forward;
        Debug.DrawRay(origin, direction * 5f, Color.yellow);
        Ray ray = new Ray(origin, direction);
        var multipleHits = Physics.RaycastAll(ray);
        foreach (var raycastHit in multipleHits)
        { raycastHit.collider.GetComponent<Renderer>().material.color = allColor; }
    }
}
