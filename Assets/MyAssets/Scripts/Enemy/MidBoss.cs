using UnityEngine;

public class MidBoss : Enemy
{
    [SerializeField] private GameObject midBossPowerup;

    // TODO powerup
    // Increase lives and weapons when boss wave starts
    // Get previous values when wave finishes
    // Check waves 

    protected override void SetInitialValues()
    {
        isMidBoss = true;
        isVulnerable = false;
        animController = GetComponent<Animator>(); // modify based on state
        // Launch powerup
        var startPos = new Vector2(7, 0);
        transform.position = startPos;
        animController.Play("midBossGood");
        Invoke(nameof(EnemyVulnerable), 1f);

        UpdateBossState(EnemyState.Good);
    }

    private void EnemyVulnerable() => isVulnerable = true;

}
