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
        if (other.CompareTag("Enemy") || other.CompareTag("MidBoss") || other.CompareTag("FinalBoss"))
        {
            Debug.Log("hitting enemy");
            var enemy = other.GetComponent<Enemy>();
            if(enemy != null) enemy.LaserDamagedEnemy();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.name == "RightCollider") gameObject.SetActive(false);
    }
}