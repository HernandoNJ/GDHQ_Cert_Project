using UnityEngine;
using static UnityEngine.Debug;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Patroling, Chasing, Attacking, Death
    }

    public EnemyState currentEnemyState;

    private void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Patroling:
                Log("Patroling");
                if (Time.time > 5)
                    // Change enemy state
                    currentEnemyState = EnemyState.Attacking;
                break;
            case EnemyState.Chasing:
                Log("Chasing");
                if (Time.time > 5)
                    currentEnemyState = EnemyState.Attacking;
                break;
            case EnemyState.Attacking:
                Log("Attacking"); break;
            case EnemyState.Death:
                Log("Death"); break;
        }

        // Modify enemy state
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentEnemyState = EnemyState.Patroling;
        }
    }
}
