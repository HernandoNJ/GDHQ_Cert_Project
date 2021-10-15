using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int health = 2;
    [SerializeField] private Vector2 startPosition;
    
    private GameManager gM_Instance;

    private void Start()
    {
        gM_Instance = GameManager.Instance;
        UIManager.Instance.LivesAmount = health;
        transform.position = startPosition;
        
        // Increase speed and health with difficulty
        speed += gM_Instance.GetCurrentDifficulty();
        health += gM_Instance.GetCurrentDifficulty();
    }

    private void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
        if (Input.GetKeyDown(KeyCode.T)) WeaponPowerup();
        if (Input.GetKeyDown(KeyCode.Y)) TestScoreUI();
    }

    private void MovePlayer()
    {
        var xPos = transform.position.x;
        var yPos = transform.position.y;
        var moveVH = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position = new Vector2(Mathf.Clamp(xPos, -7.5f, 5), Mathf.Clamp(yPos, -2.5f, 4.3f));
        transform.Translate(moveVH * speed * Time.deltaTime);
    }

    private void Shoot() => Weapons.Instance.ShootLaser();

    private void WeaponPowerup()
    {
        health++;
        Weapons.Instance.UpdateActiveWeapons(1);
    }

    public void Damage(int value)
    {
        health -= value;
        Weapons.Instance.UpdateActiveWeapons(-value);

        if (health == 0) gM_Instance.GameOver();
    }

    public int scoreToUpdate;

    private void TestScoreUI()
    {
        scoreToUpdate += 10;
        UIManager.Instance.UpdateScore(scoreToUpdate);
    }
}
