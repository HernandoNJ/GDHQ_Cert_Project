using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int weaponIncrement;
    [SerializeField] private float speed;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private Vector2 moveDirection;
    public AudioClip powerupSound;
    
    public static event Action<int> OnPowerupGot;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        OnPowerupGot?.Invoke(weaponIncrement);
        AudioSource.PlayClipAtPoint(powerupSound,transform.position);
        Destroy(gameObject);
    }
    
    public void SetWeaponIncrement(int value) => weaponIncrement = value;
}