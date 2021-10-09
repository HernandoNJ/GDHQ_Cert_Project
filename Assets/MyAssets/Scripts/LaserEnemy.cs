using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform laserParent;

    private void Start()
    {
        laserParent = GameObject.Find("SpawnedObjectsParent").GetComponent<Transform>();
        transform.SetParent(laserParent);
    }

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
        else if (other.CompareTag("Laser")) other.gameObject.SetActive(false);
        else if(other.gameObject.name == "LeftCollider") Destroy(gameObject);
    }
}
