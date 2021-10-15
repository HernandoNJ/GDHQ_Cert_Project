using UnityEngine;

public class EnemyDoubleLaser : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null) player.Damage(1);
            Destroy(gameObject);
        }
    }
}
