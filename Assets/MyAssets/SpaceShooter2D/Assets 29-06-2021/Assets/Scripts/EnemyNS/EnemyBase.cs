using System;
using Interfaces;
using Starting;
using UnityEngine;

namespace EnemyNS
{
public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    public EnemyData enemyData;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool isAlive;
    [SerializeField] protected float currentHealth;
    [SerializeField] private float fireRate;
    [SerializeField] protected float enemySpeed;
    [SerializeField] private float timeToFire;
    [SerializeField] private int scorePoints;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected int weaponIndex;

    public static event Action OnEnemyDestroyed;

    private void Start()
    {
        ConfigureEnemy(enemyData);
    }

    private void Update()
    {
        MoveEnemy();
        FireWeapon();
    }

    protected virtual void ConfigureEnemy(EnemyData _data)
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = _data.maxHealth;
        scorePoints = _data.scorePoints;
        enemyType = _data.enemyType;
        explosionPrefab = _data.explosionPrefab;
        firePoint = gameObject.transform.Find("FirePoint").transform;
        fireRate = _data.fireRate;
        isAlive = true;
        enemySpeed = _data.speed;
        weapon = _data.weapons[weaponIndex];
    }

    protected virtual void MoveEnemy()
    {
        transform.Translate(Vector2.down * (enemySpeed * Time.deltaTime));
        CheckBottomPosition();
    }

    protected void CheckBottomPosition()
    {
        if (transform.position.y <= -6.0f)
            transform.position = new Vector2(UnityEngine.Random.Range(-8.0f, 8.0f), 5.0f); // Reuse enemy in random pos.x
    }

    protected virtual void FireWeapon()
    {
        if (!(Time.time > timeToFire) || !isAlive) return;

        Instantiate(weapon, firePoint.transform.position, Quaternion.identity);
        timeToFire = Time.time + fireRate;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth > 0) return;
        isAlive = false;
        EnemyDestroyed();
    }

    private void EnemyDestroyed()
    {
        PlayerStart.onAddScore?.Invoke(scorePoints); // todo: remove later
        OnEnemyDestroyed?.Invoke();

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        enemySpeed = 0f;
        animator.SetTrigger("OnEnemyDestroyed");
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        gameObject.SetActive(false); // TODO: fix added because enemy was hitting the player twice
        Destroy(gameObject, 2.0f);
    }
}
}
