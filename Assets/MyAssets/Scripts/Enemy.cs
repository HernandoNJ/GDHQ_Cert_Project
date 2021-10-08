using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private string enemyDifficulty;
    private int health;
    [SerializeField] private float speed;

    private void OnEnable()
    {
        enemyDifficulty = GameManager.Instance.GetDifficulty();
    }

    private void Start()
    {
        SetHealth();
    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void SetHealth()
    {
        switch (enemyDifficulty)
        {
            case "Easy": health = 1; break;
            case "Medium": health = 2; break;
            case "Hard": health = 3; break;
            default: health = 1; break;
        }
    }
    
    public void Damage()
    {
        health--;
        if(health == 0) Destroy(gameObject);
    }
    
    



}
