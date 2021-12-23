using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int playerLives = 2;
    [SerializeField] private int maxPlayerLives;
    [SerializeField] private Vector2 startPosition;
    
    private UIManager uiManager;
    private GameManager gameManager;
    
    public int scoreToUpdate;

    private void OnEnable() => Powerup.PowerupGot += IncreaseLives;
    private void OnDisable() => Powerup.PowerupGot -= IncreaseLives;

    private void IncreaseLives()
    {
        if (playerLives > maxPlayerLives) return;
        playerLives++;
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
        if (Input.GetKeyDown(KeyCode.T)) WeaponPowerup(1);
        if (Input.GetKeyDown(KeyCode.Y)) TestScoreUI();
    }

    private void MovePlayer()
    {
        var xPos = transform.position.x;
        var yPos = transform.position.y;
        var moveVH = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position = new Vector2(Mathf.Clamp(xPos, -7.5f, 5), Mathf.Clamp(yPos, -2.5f, 4.3f));
        transform.Translate(moveVH.normalized * speed * Time.deltaTime);
    }

    private void Shoot() => Weapons.Instance.ShootLaser();

    private void WeaponPowerup(int value)
    {
        playerLives++;
        Weapons.Instance.UpdateActiveWeapons(value);
    }

    public void Damage(int value)
    {
        playerLives -= value;
        Weapons.Instance.UpdateActiveWeapons(-value);

        if (playerLives == 0) gameManager.GameOver();
    }

    // TODO just for testing. remove later
    private void TestScoreUI()
    {
        scoreToUpdate += 10;
        UIManager.Instance.UpdateScore(scoreToUpdate);
    }
}
