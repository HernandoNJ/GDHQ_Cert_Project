using System;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform laserParent;

    public static event Action OnPlayerDamaged;
    
    private void Start()
    {
        laserParent = GameObject.Find("SpawnedObjectsParent").GetComponent<Transform>();
        transform.SetParent(laserParent);
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerDamaged?.Invoke();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Laser"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
