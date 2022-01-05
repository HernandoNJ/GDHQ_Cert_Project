using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int animTriggerIndex;
    [SerializeField] protected float shootCooldown;
    [SerializeField] protected bool isVulnerable;
    [SerializeField] protected bool shootEnabled;
    [SerializeField] protected bool isEnemyLevel1;
    [SerializeField] protected bool isMidBoss;
    [SerializeField] protected bool isFinalBoss;
    [SerializeField] protected bool playerDestroyed;
    [SerializeField] protected bool hasPowerup;
    [SerializeField] protected Animator animController;
    [SerializeField] protected GameObject laserPrefab;
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected GameObject powerupPrefab;
    [SerializeField] protected Transform[] firePoints;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected EnemiesSpawner enemiesSpawner;
    
    protected int health;

    public static event Action OnPlayerDamaged;
    public static event Action OnBossPlayerDamage;
    
    private void OnEnable()
    {
        enemiesSpawner = FindObjectOfType<EnemiesSpawner>();
        animTriggerIndex = enemiesSpawner.GetCurrentWave();
        GameManager.OnGameOver += PlayerDestroyed;
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
        GameManager.OnGameOver -= PlayerDestroyed;
    }
    
    private void Start()
    {
        SetInitialValues();
    }

    protected virtual void SetInitialValues()
    {
        isEnemyLevel1 = CompareTag("Enemy");
        isMidBoss = CompareTag("MidBoss");
        isFinalBoss = CompareTag("FinalBoss");
        
        gameManager = GameManager.Instance;
        
        animController = GetComponent<Animator>();
        animController.SetTrigger("animTrigger"+animTriggerIndex);

        hasPowerup = Random.value > 0.5f;
        shootEnabled = true;
        if (isEnemyLevel1) isVulnerable = false;

        StartCoroutine(ShootingRoutine());
    }

    protected void Shoot()
    {
        if (shootEnabled == false) return;

        if (isEnemyLevel1) Instantiate(laserPrefab, firePoints[0].position, Quaternion.identity);

        if (isMidBoss || isFinalBoss)
        {
            foreach (var point in firePoints) { Instantiate(laserPrefab, point.position, Quaternion.identity); }
        }
    }

    public void Damage()
    {
        if (isVulnerable == false) return;

        health--;

        if (health <= 0)
        {
            if (isEnemyLevel1)
            {
                if (hasPowerup) Instantiate(powerupPrefab, transform.position, Quaternion.identity);
                PlayerScored(-1, 1);
            }
            else if (isMidBoss) PlayerScored(-1, 10);
            else if (isFinalBoss)
            {
                PlayerScored(-1, 30);
                gameManager.FinalBossDestroyed();
            }
        }
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
        shootEnabled = false;
        GetComponent<Animator>().enabled = false;
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
                    OnPlayerDamaged?.Invoke();
                    Damage();
                }
                else if (isMidBoss || isFinalBoss)
                {
                    OnBossPlayerDamage?.Invoke();
                    Damage();
                }
            }
        }
        else if (other.gameObject.name == "LeftCollider") EnemyDestroyed(-1);
    }

    protected virtual IEnumerator ShootingRoutine()
    {
        while (gameObject.activeInHierarchy && playerDestroyed == false)
        {
            yield return new WaitForSeconds(shootCooldown);
            Shoot();
        }
    }
    
    private void EnemyDestroyed(int enemiesAmount)
    {
        gameManager.OnEnemyDestroyed(enemiesAmount);
        Destroy(gameObject);
    }

    private void PlayerDestroyed()
    {
        playerDestroyed = true;
    }
    
 
}
