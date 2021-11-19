using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float speed;
    [SerializeField] private float shootCooldown;
    [SerializeField] private bool isVulnerable;
    [SerializeField] private bool shootEnabled;
    [SerializeField] private bool isEnemyLevel1;
    [SerializeField] private bool isMidBoss;
    [SerializeField] private bool isFinalBoss;

    [SerializeField] private Animator enemyAnimController;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform[] firePoints;

    [SerializeField] private GameManager gameManager;
    private int health;

    // TODO Create enemies animations with EnemyAnim + 1,2,3,4...etc  enemyAnimController
    // TODO enemies with powerups
    // TODO set mid boss and final boss movement with animation and shooting 

    private void Start()
    {
        isEnemyLevel1 = CompareTag("Enemy");
        isMidBoss = CompareTag("MidBoss");
        isFinalBoss = CompareTag("FinalBoss");
        gameManager = GameManager.Instance;

        shootEnabled = true;
        if (isEnemyLevel1) isVulnerable = false;
        InvokeRepeating(nameof(Shoot), 0.1f, shootCooldown);
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (isEnemyLevel1) transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (shootEnabled == false) return;

        if (isEnemyLevel1) Instantiate(laserPrefab, firePoints[0].position, Quaternion.identity);

        if (isMidBoss || isFinalBoss)
        {
            foreach (var point in firePoints)
            {
                Instantiate(laserPrefab, point.position, Quaternion.identity);
            }
        }
    }

    // Enemy health reduced
    public void Damage()
    {
        if (isVulnerable == false) return;

        health--;
        
        if (health <= 0)
        {
            if (isEnemyLevel1) PlayerScored(-1,1);
            else if (isMidBoss) PlayerScored(-1,10);
            else if (isFinalBoss)
            {
                PlayerScored(-1,30);
                gameManager.FinalBossDestroyed();
            }
        }
    }

    private void EnemyDestroyed(int enemiesAmount)
    {
        gameManager.OnEnemyDestroyed(enemiesAmount);
        Destroy(gameObject);
    }
    
    private void PlayerScored(int enemiesAmount, int score)
    {
        gameManager.OnPlayerScored(enemiesAmount, score);
        ExplodeEnemy();
    }

    private void ExplodeEnemy()
    {
        DisableComponents();
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.7f);
    }
    
    private void DisableComponents()
    {
        speed = 0;
        shootEnabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ActivateEnemiesCollider")
        {
            if (isEnemyLevel1) isVulnerable = true;
        }

        else if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();

            if (player != null)
            {
                if (isEnemyLevel1)
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
        else if (other.gameObject.name == "LeftCollider") EnemyDestroyed(-1);
    }
}
