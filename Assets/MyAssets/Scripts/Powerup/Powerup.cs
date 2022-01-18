using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int healthIncrement;
    [SerializeField] private int weaponIncrement;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool moveEnabled;

    public static event Action<int,int> OnPowerupGot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3.0f);
    }

    private void FixedUpdate()
    {
        if (!moveEnabled) return;
        rb.AddForceAtPosition(moveDirection * speed,transform.position,ForceMode2D.Force);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        OnPowerupGot?.Invoke(healthIncrement,weaponIncrement);
        Destroy(gameObject);
    }
    
    public void SetHealthIncrement(int value) => healthIncrement = value;
    public void SetWeaponIncrement(int value) => weaponIncrement = value;
    public void SetMoveEnabled(bool value) => moveEnabled = value;
}