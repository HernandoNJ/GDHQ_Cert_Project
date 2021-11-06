using System;
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

    [SerializeField] private Animator enemyAnimController;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform[] firePoints;

    private GameManager gm_Instance;
    private EnemiesSpawner enemiesSpawner;
    private int health;

    public static event Action OnFinalBossDestroyed;

    // TODO Create enemies animations with EnemyAnim + 1,2,3,4...etc
    // TODO enemies with powerups
    // TODO set mid boss and final boss movement with animation and shooting 

    private void Start()
    {
        isEnemy = CompareTag("Enemy");
        isMidBoss = CompareTag("MidBoss");
        isFinalBoss = CompareTag("FinalBoss");
        gm_Instance = GameManager.Instance;
        enemiesSpawner = EnemiesSpawner.Instance;

        // Increase speed with difficulty
        speed += gm_Instance.GetCurrentDifficulty();
        health = gm_Instance.GetCurrentDifficulty();
        damageAmount = gm_Instance.GetCurrentDifficulty();

        shootEnabled = true;
        shootCooldown -= gm_Instance.GetCurrentDifficulty() * 0.2f;
        if (isEnemy) isVulnerable = false;
        InvokeRepeating(nameof(Shoot), 0.1f, shootCooldown);
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (isEnemy) transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (shootEnabled == false) return;

        if (isEnemy) Instantiate(laserPrefab, firePoints[0].position, Quaternion.identity);

        if (isMidBoss || isFinalBoss)
        {
            foreach (var point in firePoints) { Instantiate(laserPrefab, point.position, Quaternion.identity); }
        }
    }

    public void Damage()
    {
        if (isVulnerable == false) return;

        health--;

        if (health == 0)
        {
            if (isEnemy) UIManager.Instance.UpdateScore(1);
            else if (isMidBoss) UIManager.Instance.UpdateScore(10);
            else if (isFinalBoss)
            {
                UIManager.Instance.UpdateScore(30);
                OnFinalBossDestroyed?.Invoke();
            }
        }
        
        BasicEnemyDestroyed();
    }
    
    private void DisableComponents()
    {
        speed = 0;
        shootEnabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void BasicEnemyDestroyed()
    {
        enemiesSpawner.EnemiesCounter--;
        DisableComponents();
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.7f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ActivateEnemiesCollider")
        {
            if (isEnemy) isVulnerable = true;
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
