using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Good,
        Regular,
        Bad
    }
    
    [SerializeField] protected bool isVulnerable;
    [SerializeField] protected int health;
    [SerializeField] protected bool isEnemyLevel1;
    [SerializeField] protected int enemyL1ScorePoints;
    [SerializeField] protected bool isMidBoss;
    [SerializeField] protected int midBossScorePoints;
    [SerializeField] protected bool isFinalBoss;
    [SerializeField] protected int finalBossScorePoints;
    
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected GameObject powerupPrefab;
    [SerializeField] protected GameObject shield;
    [SerializeField] protected Animator animController;
    [SerializeField] protected EnemiesSpawner enemiesSpawnerGO;

    public static event Action<int> OnMidOrFinalBossDamagedPlayer;
    public static event Action<EnemyState> OnBossStateChanged;
    public static event Action<int> OnEnemyDestroyed;
    public static event Action OnMidBossDestroyed;
    public static event Action OnFinalBossDestroyed;
    public static event Action<int> OnEnemyL1DamagedPlayer;
    public static event Action<bool> OnShootingStateChanged;
  
    // todo 1: check enemy shooting
    
    private void OnEnable()
    {
        enemiesSpawnerGO = FindObjectOfType<EnemiesSpawner>();
        GameManager.OnGameOver += StopShooting;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= StopShooting;
        StopAllCoroutines();
    }

    private void Start()
    {
        SetInitialValues();
    }

    protected virtual void SetInitialValues()
    {
        animController = GetComponent<Animator>();
        StartShooting();
        CheckBossTag();
    }

    private void CheckBossTag()
    {
        if (gameObject.CompareTag("EnemyLevel1")) isEnemyLevel1 = true;
        else if (gameObject.CompareTag("MidBoss")) isMidBoss = true;
        else if (gameObject.CompareTag("FinalBoss")) isFinalBoss = true;
        else Debug.LogWarning("Set a valid enemy tag");
    }
    
    protected void SetEnemyVulnerable(bool isVulnerableArg)
    {
        isVulnerable = isVulnerableArg;
    }
    
    protected void UpdateBossState(EnemyState enemyStateArg)
    {
        switch (enemyStateArg)
        {
            case EnemyState.Good:
                UpdateBossState2(Color.green, 1f,enemyStateArg);
                break;
            case EnemyState.Regular:
                UpdateBossState2(Color.yellow, 1.2f,enemyStateArg);
                break;
            case EnemyState.Bad:
                UpdateBossState2(Color.red, 1.4f,enemyStateArg);
                break;
            default:
                Debug.LogWarning("Set a valid value in UpdateMidBossState");
                break;
        }
    }

    private void UpdateBossState2(Color colorArg, float animSpeed, EnemyState enemyStateValue)
    {
        shield.GetComponent<Renderer>().material.color = colorArg;
        animController.speed = animSpeed;
        OnBossStateChanged?.Invoke(enemyStateValue);
    }
    
    protected virtual void Damage()
    {
        if (isVulnerable == false) return;
        health--;

        if (health > 0)
        {
            if(isEnemyLevel1) Debug.Log("Enemy L1 damaged"); // TODO just for testing
            else if (isMidBoss || isFinalBoss)
            {
                if (health is < 30 and > 10) UpdateBossState(EnemyState.Regular);
                else if (health < 10) UpdateBossState(EnemyState.Bad);
            }
        }
        else
        {
            EnemyDestroyed();
            OnEnemyDestroyed?.Invoke(SetScorePoints());
            if(isFinalBoss) OnFinalBossDestroyed?.Invoke();
        }
    }

    public void LaserDamagedEnemy() => Damage();

    protected void EnemyDestroyed()
    {
        DisableComponents();
        
        if(isEnemyLevel1) OnEnemyDestroyed?.Invoke(SetScorePoints()); // todo modify when enemy collides with obstacle or with player laser

        if (isMidBoss)
        {
            OnEnemyDestroyed?.Invoke(SetScorePoints());
            OnMidBossDestroyed?.Invoke();
        }
        else if (isFinalBoss)
        {
            OnEnemyDestroyed?.Invoke(SetScorePoints());
            OnFinalBossDestroyed?.Invoke();
        }
        
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(gameObject, 0.7f);
    }

    private void DisableComponents()
    {
        StopShooting();
        GetComponent<Animator>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();

            if (player == null) return;
            
            if (isEnemyLevel1) OnEnemyL1DamagedPlayer?.Invoke(1);
            else if (isMidBoss || isFinalBoss) OnMidOrFinalBossDamagedPlayer?.Invoke(2);

            Damage();
        }
    }
    
    private int SetScorePoints()
    {
        var scorePoints = 0;
        if (isEnemyLevel1) scorePoints =  enemyL1ScorePoints;
        if (isMidBoss) scorePoints = midBossScorePoints;
        if (isFinalBoss) scorePoints = finalBossScorePoints;
        return scorePoints;
    }

    private void StartShooting() => EnemyShoot(true);
    private void StopShooting() => EnemyShoot(false);
    
    private void EnemyShoot(bool isShootEnabled)
    {
        OnShootingStateChanged?.Invoke(isShootEnabled);
    }
}