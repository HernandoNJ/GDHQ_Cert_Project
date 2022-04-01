using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Increased in each wave")]
    [SerializeField] private float speed;
    [SerializeField] private int currentLives;
    [SerializeField] private int maxLives;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private bool l1Active; // Reduce weapons only if active

    // Required for SetNewEnemyValues()
    // L1: Enemy basic, L2: MidBoss, L3: FinalBoss
    private const float SpeedL1 = 5f;
    private const float SpeedL2 = 6f;
    private const float SpeedL3 = 7.5f;
    
    private const int MaxLivesL1 = 3;
    private const int MaxLivesL2 = 5;
    private const int MaxLivesL3 = 7;

    private UIManager uiManager;
    private GameManager gameManager;

    public static event Action<int> OnAddWeapons;
    public static event Action<int> OnReduceWeapons;
    public static event Action OnPlayerShooting;

    private void OnEnable()
    {
        EnemiesSpawner.OnEnemyL1WaveStarted += EnemyL1WaveStarted;
        EnemiesSpawner.OnMidBossWaveStarted += MidBossWaveStarted;
        EnemiesSpawner.OnFinalBossWaveStarted += FinalBossWaveStarted;
        Enemy.OnEnemyL1DamagedPlayer += EnemyLaserDamagedPlayer;
        Enemy.OnMidOrFinalBossDamagedPlayer += MidOrFinalBossDamagedPlayer;
        Enemy.OnMidBossDestroyed += EnemyL1WaveStarted;
        LaserEnemy.EnemyLaserDamagedPlayer += EnemyLaserDamagedPlayer;
        Powerup.OnPowerupGot += PowerupGot;
    }

    private void OnDisable()
    {
        EnemiesSpawner.OnEnemyL1WaveStarted -= EnemyL1WaveStarted;
        EnemiesSpawner.OnMidBossWaveStarted -= MidBossWaveStarted;
        EnemiesSpawner.OnFinalBossWaveStarted -= FinalBossWaveStarted;
        Enemy.OnEnemyL1DamagedPlayer -= EnemyLaserDamagedPlayer;
        Enemy.OnMidOrFinalBossDamagedPlayer -= MidOrFinalBossDamagedPlayer;
        Enemy.OnMidBossDestroyed -= EnemyL1WaveStarted;
        LaserEnemy.EnemyLaserDamagedPlayer -= EnemyLaserDamagedPlayer;
        Powerup.OnPowerupGot -= PowerupGot;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        uiManager.LivesAmount = currentLives;
        transform.position = startPosition;
    }

    private void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    private void MovePlayer()
    {
        var xPos = transform.position.x;
        var yPos = transform.position.y;
        var moveVH = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position = new Vector2(Mathf.Clamp(xPos, -7.5f, 5), Mathf.Clamp(yPos, -2.5f, 4.3f));
        transform.Translate(moveVH.normalized * (speed * Time.deltaTime));
    }

    private void Shoot() => OnPlayerShooting?.Invoke();

    private void SetPlayerLives(int value)
    {
        if (currentLives >= maxLives) return;

        currentLives += value;
        if (currentLives >= maxLives) currentLives = maxLives;
    }

    private void CheckIfPlayerDestroyed()
    {
        if (currentLives <= 0)
        {
            gameManager.GameOver();
            Destroy(gameObject,2f);
        }
    }

    private void PowerupGot(int weaponsAdd)
    {
        OnAddWeapons?.Invoke(weaponsAdd);
    }

    private void EnemyL1WaveStarted()
    {
        l1Active = true;
        SetNewEnemyValues(SpeedL1, MaxLivesL1);
    }

    private void MidBossWaveStarted() => SetNewEnemyValues(SpeedL2, MaxLivesL2);

    private void FinalBossWaveStarted() => SetNewEnemyValues(SpeedL3, MaxLivesL3);

    private void SetNewEnemyValues(float speedArg, int maxLivesArg)
    {
        speed = speedArg;
        maxLives = maxLivesArg;
        currentLives = maxLives;
    }

    private void EnemyLaserDamagedPlayer(int value)
    {
        SetPlayerLives(-value);
        CheckIfPlayerDestroyed();
        
        if(l1Active) OnReduceWeapons?.Invoke(1);
    }

    private void MidOrFinalBossDamagedPlayer(int value) => SetPlayerLives(-value);
}
