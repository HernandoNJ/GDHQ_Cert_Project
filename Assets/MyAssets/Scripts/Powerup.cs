using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int healthIncrement;
    [SerializeField] private int weaponIncrement;

    public static event Action PowerupGot;

    private void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PowerupGot?.Invoke();
        Destroy(gameObject);
    }
}