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
    
    [SerializeField] protected int health;
    [SerializeField] protected int scorePoints;
    [SerializeField] private float shootingDelay;
    [SerializeField] private float vulnerableDelay;
    [SerializeField] protected bool isVulnerable;
    [SerializeField] protected bool hasPowerup;
    [SerializeField] protected bool isEnemyLevel1;
    [SerializeField] protected bool isMidBoss;
    [SerializeField] protected bool isFinalBoss;
    
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected GameObject powerupPrefab;
    [SerializeField] protected GameObject shield;
    [SerializeField] protected Animator animController;
    
    public static event Action<int> OnMidOrFinalBossDamagedPlayer;
    public static event Action<EnemyState> OnBossStateChanged;
    public static event Action<int> OnEnemyDestroyed;
    public static event Action OnEnemyL1Destroyed;
    public static event Action OnMidBossDestroyed;
    public static event Action OnFinalBossDestroyed;
    public static event Action<int> OnEnemyL1DamagedPlayer;
    public static event Action<bool> OnShootingStateChanged;
  
    // todo 1: check enemy shooting
    
    private void OnEnable()
    {
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
        SetBoolVariables(false);
        CheckEnemyTag();
        Invoke(nameof(StartShooting),shootingDelay);
        Invoke(nameof(SetVulnerable),vulnerableDelay);
    }

    private void SetBoolVariables(bool boolArg)
    {
        isVulnerable = boolArg;
        shield.gameObject.SetActive(boolArg);
        explosionPrefab.SetActive(boolArg);
    }

    private void CheckEnemyTag()
    {
        if (gameObject.CompareTag("EnemyLevel1")) isEnemyLevel1 = true;
        else if (gameObject.CompareTag("MidBoss")) isMidBoss = true;
        else if (gameObject.CompareTag("FinalBoss")) isFinalBoss = true;
        else Debug.LogWarning("Set a valid enemy tag");
    }
    
    private void SetVulnerable() => isVulnerable = true;
    
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
    
    protected void Damage()
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
        else EnemyDestroyed();
    }

    protected void EnemyDestroyed()
    {
        CheckIfHasPowerup();
        DisableComponents();
        
        if (isEnemyLevel1) OnEnemyL1Destroyed?.Invoke();
        else if (isMidBoss) OnMidBossDestroyed?.Invoke();
        else if (isFinalBoss) OnFinalBossDestroyed?.Invoke();
        
        OnEnemyDestroyed?.Invoke(GetScorePoints()); // todo modify when enemy collides with obstacle or with player laser
        explosionPrefab.SetActive(true);
        Destroy(gameObject, 1f);
    }

    private void CheckIfHasPowerup()
    {
        if (hasPowerup) Instantiate(powerupPrefab, transform.position, Quaternion.identity);
    }

    private void DisableComponents()
    {
        StopShooting();
        GetComponent<Animator>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy collides with the player
        if (other.CompareTag("Player"))
        {
            if (isEnemyLevel1) OnEnemyL1DamagedPlayer?.Invoke(1);
            else if (isMidBoss || isFinalBoss) OnMidOrFinalBossDamagedPlayer?.Invoke(2);
            Damage();
        }
        else if (other.CompareTag("Outbound") || other.CompareTag("PlayerLaser"))
        {
            Debug.Log("enemy hit: " + other.gameObject.name);
            Damage();
        } 
    }
    
    private int GetScorePoints() => scorePoints;

    private void StartShooting() => EnemyShoot(true);
    private void StopShooting() => EnemyShoot(false);
    
    private void EnemyShoot(bool isShootEnabled)
    {
        OnShootingStateChanged?.Invoke(isShootEnabled);
    }
}