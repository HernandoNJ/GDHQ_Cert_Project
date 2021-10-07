using System.Collections.Generic;
using UnityEngine;

namespace EnemyNS
{
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/Enemy/EnemyData", order = 0)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public string description;
    public int scorePoints;
    public float speed;
    public float fireRate;
    public int maxHealth;
    public List<GameObject> weapons = new List<GameObject>();
    public GameObject explosionPrefab;
    
}

public enum EnemyType { Default, Basic, DoubleShooter, Chaser, Aggressive, 
ShotAvoider, Shielded, Boss }
}
