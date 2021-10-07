using Managers;
using UnityEngine;
using static UnityEngine.Debug;

namespace Starting
{
public class EnemyStart : MonoBehaviour
{
    public enum EnemyType
    {
        BasicEnemy, DoubleShotEnemy, ShieldedEnemy, AggressiveEnemy, ChaserEnemy, BackShootEnemy, AvoidShotEnemy,
        BossEnemy, Null
    }

    private enum EnemyWeapon { Laser, DoubleLaser, Rocket, BackwardLaser, Null }
    private enum EnemyMove { Normal, ZigZag, ChasePlayer, AvoidShot, Null };

    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject doubleLaserPrefab;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject rechargedLaser;
    [SerializeField] private GameObject shield;
    [SerializeField] private PlayerStart player;
    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private bool isEnemyAlive;
    [SerializeField] private bool hasShield;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float timeTime;
    [SerializeField] private int leftRightSpeed;
    [SerializeField] private int enemyDirection = 1;

    private float fireRate = 3f;
    private float canFire;

    public void my()
    {
        Log("hi");
    }

    private void Start()
    {
        player = GameObject.Find("PlayerStart").GetComponent<PlayerStart>();
        if (player == null) LogError("There is not PlayerStart script in player");

        anim = GetComponent<Animator>();
        if (anim == null) LogError("anim is null in Enemy script");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) LogError("audioSource is null in Enemy script");

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null) LogError("spawnmanager is null in Enemy script");

        isEnemyAlive = true;
        leftRightSpeed = 1;
        shield.SetActive(false);
    }

    private void Update()
    {
        timeTime = Time.time;
        //MoveEnemy();
        MoveEnemy2();
        ShotDoubleLaser();
    }

    private void MoveEnemy()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        CheckBottomPosition();
    }

    private void MoveEnemy2()
    {
        Vector2 vec = new Vector2(leftRightSpeed * enemyDirection, -1 * speed);
        transform.Translate(vec * Time.deltaTime);

        // set xpos = pos x **** if posx > xpos + 2
        Vector2 xPos = transform.position;

        if (transform.position.x >= 2f) enemyDirection = -1;
        if (transform.position.x <= -2f) enemyDirection = 1;

        CheckBottomPosition();
    }

    private void CheckBottomPosition()
    {
        if (transform.position.y <= -6.0f)
            // Reuse enemy in random pos.x
            transform.position = new Vector2(UnityEngine.Random.Range(-8.0f, 8.0f), 5.0f);
    }

    public void ShotDoubleLaser()
    {
        if (Time.time > canFire && isEnemyAlive)
        {
            Instantiate(doubleLaserPrefab, transform.position, Quaternion.Euler(Vector2.down));
            fireRate = UnityEngine.Random.Range(3f, 7f);
            canFire = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision if other is an Enemy
        if (other.transform.parent != null && other.transform.parent.CompareTag("EnemyLaser"))
            return;

        if (other.CompareTag("PlayerStart"))
        {
            player.TakeDamage(1);
            speed = 0f;
            anim.SetTrigger("OnEnemyDestroyed");
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            gameObject.SetActive(false); // added because enemy was hitting the player twice
            Destroy(gameObject, 2.0f);
        }
    }

    // Called by a laser when hit
    public void OnEnemyDestroyed()
    {
        isEnemyAlive = false;
        anim.SetTrigger("OnEnemyDestroyed");
        speed = 0f;
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, 2.0f);
    }
}
}
