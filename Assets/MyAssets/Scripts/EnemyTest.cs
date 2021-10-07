using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    private string enemyDifficulty;
    private int health;

    private void OnEnable()
    {
        enemyDifficulty = GameManager.Instance.GetDifficulty();
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
    
    
}
