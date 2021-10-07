using System;
using UnityEngine;
using Training.Others;

public class EndTrigger : MonoBehaviour
{

    public static event Action<Vector3, Mech> onEndReachedAction;

    public static Func<GameObject> onGameObjectReturned;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            onGameObjectReturned =
                () => GameObject.FindGameObjectWithTag("Enemy");
        onGameObjectReturned?.Invoke();

        if (onGameObjectReturned != null)
        {
            GameObject newMech = onGameObjectReturned();
            Debug.Log("new Mech name: " + newMech.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var mech = other.GetComponent<Mech>();

        if (other.CompareTag("Enemy"))
            onEndReachedAction?.Invoke(transform.position, mech);
    }
}
