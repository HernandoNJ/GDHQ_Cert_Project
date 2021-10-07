using Starting;
using UnityEngine;

public class EnemyDoubleLaserStart : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private void Update()
    {
        MoveDoubleLaser();
    }

    public void MoveDoubleLaser()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -7.5f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerStart"))
        {
            PlayerStart player = other.GetComponent<PlayerStart>();
            if (player != null) player.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
