using UnityEngine;
using static UnityEngine.Debug;

namespace Starting
{
public class PowerUpStart : MonoBehaviour
{
    [SerializeField] private AudioClip powerupSound;

    [SerializeField] private float speed = 3f;
    [SerializeField] private int powerupID;

    private void Update()
    {
        MovePowerup();

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void MovePowerup()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        AudioSource.PlayClipAtPoint(powerupSound, transform.position, 0.3f);

        var player = other.GetComponent<PlayerStart>();

        if (player != null)
        {   
            // TODO set powerups as enum
            switch (powerupID)
            {
                case 0:
                    player.ActivateTripleLaser();
                    break;
                case 1:
                    player.ActivateSpeedBoost();
                    break;
                case 2:
                    player.ActivateShield();
                    break;
                case 3:
                    player.RefillAmmo();
                    break;
                case 4:
                    player.RecoverHealth();
                    break;
                case 5:
                    player.ActivateMultipleShot();
                    break;
                case 6:
                    player.TakeDamage(2);
                    break;
                default:
                    Log("Default message in switch");
                    break;
            }
        }
        else LogError("There is no PlayerStart script in player (other)");

        Destroy(gameObject);
    }
}
}
