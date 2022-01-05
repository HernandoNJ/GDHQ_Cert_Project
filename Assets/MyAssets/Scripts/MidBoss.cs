using System;
using System.Collections;
using UnityEngine;

public class MidBoss : Enemy
{
    private enum MidBossState
    {
        Good,
        Regular,
        Bad
    };

    [SerializeField] private GameObject midBossPowerup; // increase 7 lives
    [SerializeField] private GameObject shield; // increase 7 lives
    [SerializeField] private float speed;
    [SerializeField] private Color actualColor;

    // TODO create a shield
    // for each enum state:
    // modify shield color, speed, cooldown and firepoint

    protected override void SetInitialValues()
    {
        speed = 1f;
        health = 50;
        isMidBoss = true;
        gameManager = GameManager.Instance;
        animController = GetComponent<Animator>(); // modify based on state
        shootEnabled = true;
        shootCooldown = 0.2f;
        // Launch powerup
        var startPos = new Vector2(7, 0);
        transform.position = startPos;
        animController.Play("midBossGood");
        Invoke(nameof(MidBossVulnerable), 1f);

        StartCoroutine(ShootingRoutine());
    }

    private void MidBossVulnerable()
    {
        isVulnerable = true;
    }

    private void UpdateMidBossState(MidBossState state)
    {
        // setup state
        // modify shield color, speed, cooldown and firepoint depending on health
    }

    protected override IEnumerator ShootingRoutine()
    {
        while (gameObject.activeInHierarchy && playerDestroyed == false)
        {
            for (float i = 0; i < 0.5f; i += 0.1f)
            {
                yield return new WaitForSeconds(shootCooldown);
                Shoot();
            }

            yield return new WaitForSeconds(1.5f);
        }
        
        animController.Play("Empty");
    }
}