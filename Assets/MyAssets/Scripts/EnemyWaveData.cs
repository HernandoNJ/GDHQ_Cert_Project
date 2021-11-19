using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWaveData", menuName = "Scriptable Obj/EnemyWaveData")]
public class EnemyWaveData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int maxEnemies;
    
}
