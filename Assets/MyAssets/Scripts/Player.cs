using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int currentLives = 1;
    [SerializeField] private int maxLives = 1;
    [SerializeField] private int enemyL1WaveMaxLives;
    [SerializeField] private int midBossWaveMaxLives;
    [SerializeField] private int finalBossWaveMaxLives;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private bool enemyL1Active;
    [SerializeField] private bool midBossActive; // todo for testing
    [SerializeField] private bool finalBossActive; // todo for testing

    private UIManager uiManager;
    private GameManager gameManager;

    public static event Action<int> OnAddWeapons;
    public static event Action<int> OnReduceWeapons;
    public static event Action OnPlayerShooting;

    private void OnEnable()
    {
        Enemy.OnEnemyL1DamagedPlayer += PlayerDamaged;
        Enemy.OnMidOrFinalBossDamagedPlayer += MidOrFinalBossDamagedPlayer;
        Enemy.OnMidBossDestroyed += EnemyL1WaveStarted;
        LaserEnemy.OnPlayerDamaged += PlayerDamaged;
        MidBoss.OnMidBossWaveStarted += MidBossWaveStarted;
        Powerup.OnPowerupGot += PowerupGot;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyL1DamagedPlayer -= PlayerDamaged;
        Enemy.OnMidOrFinalBossDamagedPlayer -= MidOrFinalBossDamagedPlayer;
        Enemy.OnMidBossDestroyed -= EnemyL1WaveStarted;
        LaserEnemy.OnPlayerDamaged -= PlayerDamaged;
        MidBoss.OnMidBossWaveStarted -= MidBossWaveStarted;
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
        transform.Translate(moveVH.normalized * speed * Time.deltaTime);
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

    private void PowerupGot(int livesAdd, int weaponsAdd)
    {
        SetPlayerLives(livesAdd);
        OnAddWeapons?.Invoke(weaponsAdd);
    }

    private void EnemyL1WaveStarted()
    {
        enemyL1Active = true;
        SetNewEnemyValues(5f, enemyL1WaveMaxLives);
    }

    private void MidBossWaveStarted()
    {
        midBossActive = true;
        SetNewEnemyValues(7f, midBossWaveMaxLives);
    }

    private void FinalBossWaveStarted()
    {
        finalBossActive = true;
        SetNewEnemyValues(10f, finalBossWaveMaxLives);
    }

    private void SetNewEnemyValues(float speedArg, int maxLivesArg)
    {
        speed = speedArg;
        maxLives = maxLivesArg;
        currentLives = maxLives;
    }

    private void PlayerDamaged(int value)
    {
        SetPlayerLives(-value);
        CheckIfPlayerDestroyed();
        
        if(enemyL1Active) OnReduceWeapons?.Invoke(1);
    }

    private void MidOrFinalBossDamagedPlayer(int value)
    {
        SetPlayerLives(-value);
        Debug.Log("Boss damaged Player x 2");
    }
}
