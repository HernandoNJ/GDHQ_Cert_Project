using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Obj/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int maxEnemies;
    
}
