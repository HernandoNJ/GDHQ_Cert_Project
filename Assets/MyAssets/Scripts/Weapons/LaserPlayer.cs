using UnityEngine;

public class LaserPlayer : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyLevel1") || 
            other.CompareTag("MidBoss") || 
            other.CompareTag("FinalBoss") ||
            other.CompareTag("Outbound"))
        {
            Debug.Log("Laser player hit: " + other.gameObject.name);
            gameObject.SetActive(false);
        }
    }
}