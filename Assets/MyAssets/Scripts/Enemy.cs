using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float speed;
    [SerializeField] private float shootCooldown;
    [SerializeField] private bool isVulnerable;
    [SerializeField] private bool shootEnabled;
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool isMidBoss;
    [SerializeField] private bool isFinalBoss;
    
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform[] firePoints;

    private GameManager gM_Instance;
    private EnemiesSpawner enemySp_Instance;
    private int health;

    // TODO enemies with powerups
    // TODO set mid boss and final boss movement with coroutine and shooting 
    // TODO move enemies with animation
    
    private void Start()
    {
        isEnemy = CompareTag("Enemy");
        isMidBoss = CompareTag("MidBoss");
        isFinalBoss = CompareTag("FinalBoss");
        gM_Instance = GameManager.Instance;
        enemySp_Instance = EnemiesSpawner.Instance;

        // Increase speed with difficulty
        speed += gM_Instance.GetCurrentDifficulty();
        health = gM_Instance.GetCurrentDifficulty();
        damageAmount = gM_Instance.GetCurrentDifficulty();

        shootEnabled = true;
        isVulnerable = false;
        shootCooldown -= gM_Instance.GetCurrentDifficulty() * 0.2f;
        InvokeRepeating(nameof(Shoot), 0.1f, shootCooldown);
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
        if (shootEnabled == false) return;

        if (isEnemy) Instantiate(laserPrefab, firePoints[0].position, Quaternion.identity);

        if (isMidBoss || isFinalBoss)
        {
            foreach (var point in firePoints)
            {
                Instantiate(laserPrefab, point.position, Quaternion.identity);
            }
        }
    }

    public void Damage()
    {
        if (isVulnerable == false) return;

        health--;

        if (health == 0)
        {
            if(isEnemy) EnemyDestroyed();
            else if (isMidBoss) enemySp_Instance.MidBossDestroyed();
            else if (isFinalBoss) enemySp_Instance.FinalBossDestroyed();
            {
                //Enemy
            }
            //else if (isFinalBoss) finalBossDestroyed = true;
        }
    }

    private void DisableComponents()
    {
        speed = 0;
        shootEnabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void EnemyDestroyed()
    {
        DisableComponents();
        var explosion = Instantiate(explosionPrefab, transform.position, quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.7f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemy)
        {
            if (other.gameObject.name == "ActivateEnemiesCollider")
                isVulnerable = true;
        }

        else if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();

            if (player != null)
            {
                if (isEnemy)
                {
                    player.Damage(damageAmount);
                    Damage();
                }
                else if (isMidBoss || isFinalBoss)
                {
                    player.Damage(damageAmount * 2);
                    Damage();
                }
            }
        }
        else if (other.gameObject.name == "LeftCollider") Destroy(gameObject);
    }
}
