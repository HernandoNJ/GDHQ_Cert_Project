using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWaveData", menuName = "Scriptable Obj/EnemyWaveData")]
public class EnemyWaveData : ScriptableObject
{
    public GameObject enemyPrefab;
    public Animator animController;
    public int animationIndex;
    public int maxEnemies;
    
}
