using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int playerLives = 2;
    [SerializeField] private int maxPlayerLives;
    [SerializeField] private Vector2 startPosition;

    private UIManager uiManager;
    private GameManager gameManager;

    public static event Action OnPlayerShooting;
    
    public int scoreToUpdate;

    private void OnEnable()
    {
        Powerup.OnPowerupGot += PowerupGot;
        LaserEnemy.OnPlayerDamaged += PlayerDamaged;
        Enemy.OnEnemyL1DamagedPlayer += PlayerDamaged;
        Enemy.OnBossDamagedPlayer += BossDamagedPlayer;
    }

    private void OnDisable()
    {
        Powerup.OnPowerupGot -= PowerupGot;
        LaserEnemy.OnPlayerDamaged -= PlayerDamaged;
        Enemy.OnEnemyL1DamagedPlayer -= PlayerDamaged;
        Enemy.OnBossDamagedPlayer -= BossDamagedPlayer;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        uiManager.LivesAmount = playerLives;
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

    private void PowerupGot()
    {
        if (playerLives > maxPlayerLives) return;
        playerLives++;
        Debug.Log($"Powerup collected. lives: {playerLives}");
    }

    private void PlayerDamaged()
    {
        // TODO reduce weapons
        playerLives -= 1;
        Debug.Log($"Player damaged. lives: {playerLives}");
        if (playerLives == 0) gameManager.GameOver();
    }

    private void BossDamagedPlayer()
    {
        Debug.Log("Boss damaged Player x 2");
    }
}