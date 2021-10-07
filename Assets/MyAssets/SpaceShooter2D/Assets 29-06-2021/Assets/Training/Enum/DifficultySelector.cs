using UnityEngine;
using static UnityEngine.Debug;

public class DifficultySelector : MonoBehaviour
{
    public enum Difficulty
    {
        Easy = 1, Normal = 14, Hard = 30, Expert = 50
    }

    [SerializeField] private Difficulty currentLevel;

    private void Start()
    {
        switch (currentLevel)
        {
            case Difficulty.Easy: 
                Log("Easy selected"); break;
            case Difficulty.Normal:
                Log("Normal selected"); break;
            case Difficulty.Hard:
                Log("Hard selected"); break;
        }
    }

}
