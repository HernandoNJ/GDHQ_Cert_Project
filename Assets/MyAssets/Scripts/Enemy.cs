using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float shootCooldown;
    [SerializeField] private bool isVulnerable;
    [SerializeField] private bool shootEnabled;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform firePoint;
    
    private int health;

    private void Start()
    {
        // Increase speed with difficulty
        speed += GameManager.Instance.GetCurrentDifficulty();
        health = GameManager.Instance.GetCurrentDifficulty();

        shootEnabled = true;
        isVulnerable = false;
        shootCooldown -= GameManager.Instance.GetCurrentDifficulty() * 0.2f;
        InvokeRepeating(nameof(Shoot),0.1f,shootCooldown);
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        if(shootEnabled) 
            Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
    }

    public void Damage()
    {
        if (isVulnerable == false) return;
        
        health--;

        if (health == 0)
        {
            StartExplosion();
            Destroy(gameObject);
        }
    }

    private void DisableComponents()
    {
        speed = 0;
        shootEnabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void StartExplosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, quaternion.identity);
        Destroy(explosion,0.5f);
    } 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ActivateEnemiesCollider")
            isVulnerable = true;

        else if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if(player != null) player.Damage(1);
            DisableComponents();
            StartExplosion();
            Destroy(gameObject,0.5f);
        }
        else if (other.gameObject.name == "LeftCollider") Destroy(gameObject);
    }
}
