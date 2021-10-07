using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int health;
    [SerializeField] private Vector2 startPosition;


    private void Start()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
        if (Input.GetKeyDown(KeyCode.T)) WeaponPowerup();
    }

    private void MovePlayer()
    {
        var xPos = transform.position.x;
        var yPos = transform.position.y;
        var moveVH = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position = new Vector2(Mathf.Clamp(xPos, -5, 5), Mathf.Clamp(yPos, -3, 3));
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

        if (health == 0)
        {
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
        }
    }
}
