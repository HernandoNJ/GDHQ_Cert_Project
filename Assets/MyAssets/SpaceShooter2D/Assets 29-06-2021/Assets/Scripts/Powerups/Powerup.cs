using System;
using UnityEngine;
using static PowerupType;

public enum PowerupType
{
    Default,
    TripleLaser,
    SpeedBoost,
    Shield,
    RefillAmmo,
    RecoverHealth,
    MultipleShot,
    DamagePickup
}

namespace Powerups
{
public class Powerup : MonoBehaviour
{
    [SerializeField] private AudioClip powerupSound;
    [SerializeField] private PowerupType powerupType;
    [SerializeField] private float speed = 2.5f;

    public static event Action<PowerupType> OnWeaponPowerupCollected;
    public static event Action<PowerupType> OnHealthPowerupCollected;
    public static event Action<float> OnMovementPowerupCollected;
    public static event Action OnShieldPowerupCollected;

    private void Update()
    {
        Move();

        if (transform.position.y < -5f)
            Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        AudioSource.PlayClipAtPoint(powerupSound, transform.position, 0.3f);
        SetPowerupInfo(powerupType);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 2f);
    }

    private void SetPowerupInfo(PowerupType powerupInfo)
    {
        switch (powerupInfo)
        {
            case TripleLaser:
            case MultipleShot:
            case RefillAmmo:
                OnWeaponPowerupCollected?.Invoke(powerupInfo);
                break;
            case RecoverHealth:
            case DamagePickup:
                OnHealthPowerupCollected?.Invoke(powerupInfo);
                break;
            case Shield:
                    OnShieldPowerupCollected?.Invoke();
                    break;
            case SpeedBoost:
                OnMovementPowerupCollected?.Invoke(1.5f);
                break;
            default:
                Debug.Log("Configure powerup");
                throw new ArgumentOutOfRangeException(nameof(powerupInfo), powerupInfo, null);
        }
    }
}
}
