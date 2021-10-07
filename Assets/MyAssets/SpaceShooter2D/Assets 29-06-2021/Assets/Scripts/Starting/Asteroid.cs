using System;
using Interfaces;
using UnityEngine;

namespace Starting
{
public class Asteroid : MonoBehaviour, IDamageable
{
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameObject explosionPrefab;

    public static event Action OnAsteroidDestroyed;

    private void Start()
    {
        transform.position = Vector3.up * 3f;
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.Self);
    }

    public void TakeDamage(int damage)
    {
        OnAsteroidDestroyed?.Invoke();
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }
}
}
