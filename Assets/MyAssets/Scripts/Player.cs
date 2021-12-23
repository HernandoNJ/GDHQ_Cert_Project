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

    private void OnEnable() => Powerup.PowerupGot += PowerupCollected;
    private void OnDisable() => Powerup.PowerupGot -= PowerupCollected;

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
        if (Input.GetKeyDown(KeyCode.Space)) 
            Shoot();
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

    private void PowerupCollected()
    {
        if (playerLives > maxPlayerLives) return;
        playerLives++;
        Weapons.Instance.UpdateActiveWeapons(1);
    }

    public void Damage(int value)
    {
        playerLives -= value;
        Weapons.Instance.UpdateActiveWeapons(-value);

        if (playerLives == 0) gameManager.GameOver();
    }
}
