using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null) player.Damage(1); 
            Destroy(gameObject);
        }
        else if (other.CompareTag("Outbound")) Destroy(gameObject);
    }
}
