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

    private GameManager gM_Instance;
    private EnemiesSpawner enemySp_Instance;
    private int health;

    // TODO Create enemies animations with EnemyAnim + 1,2,3,4...etc
    // 8 for midBoss and 12 for final boss
    // TODO enemies with powerups
    // TODO set mid boss and final boss movement with animation and shooting 
    // TODO Ask Thom about using animations with Cinemachine
    // TODO Ask Thom about documentation


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
        shootCooldown -= gM_Instance.GetCurrentDifficulty() * 0.2f;
        if (isEnemy) isVulnerable = false;
        SetEnemyAnimation();
        InvokeRepeating(nameof(Shoot), 0.1f, shootCooldown);
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (isEnemy)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
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
            if (isEnemy)
            {
                EnemyDestroyed();
                UIManager.Instance.UpdateScore(1);
            }
            else if (isMidBoss)
            {
                enemySp_Instance.MidBossDestroyed();
                UIManager.Instance.UpdateScore(10);
            }
            else if (isFinalBoss)
            {
                EnemiesSpawner.FinalBossDestroyed();
                UIManager.Instance.UpdateScore(30);
            }
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
        // TODO ask Thom if there is a better way
        DisableComponents();
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.7f);
    }

    // Values 1 to 7 and 9 to 11 for basic enemy
    // 8 for mid boss and 12 for final boss
    // StartNewWave() in EnemiesSpawner modifies the anim value
    private void SetEnemyAnimation()
    {
        var animValue = enemySp_Instance.GetCurrentWave();
        enemyAnimController.Play("EnemyAnim" + animValue);
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
