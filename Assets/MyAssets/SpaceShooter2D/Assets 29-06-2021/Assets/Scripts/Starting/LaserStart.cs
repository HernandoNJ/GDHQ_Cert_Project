using Interfaces;
using UnityEngine;
using static UnityEngine.Debug;

namespace Starting
{
public class LaserStart : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damageAmount;
    [SerializeField] private PlayerStart player;

    private void Start()
    {
        player = GameObject.Find("PlayerStart").GetComponent<PlayerStart>();
        speed = 5f;
        damageAmount = 1;
    }

    private void Update()
    {
        MoveLaserUp();
    }

    private void MoveLaserUp()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));

        if (transform.position.y >= 7.5f)
        {
            Destroy(gameObject);
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<IDamageable>();

        if (iDamage == null)
        {
            Log("ITakeDamage not found in other: " + other.gameObject.name);
            return;
        }

        iDamage.TakeDamage(damageAmount);
        Destroy(gameObject);
    }
}
}
