using UnityEngine;

public class PlayerWeapons : Weapons
{
    [SerializeField] private float bossShootCooldown;
    
    private void OnEnable()
    {
        Player.OnPlayerShooting += FireWeapons;
        Powerup.OnPowerupGot += IncreaseWeapons;
        LaserEnemy.OnPlayerDamaged += DecreaseWeapons;
        Enemy.OnEnemyL1DamagedPlayer += DecreaseWeapons;
        Enemy.OnBossDamagedPlayer += DecreaseWeapons;
        EnemiesSpawner.OnBossWaveStarted += SetBossCooldown;
    }

    private void OnDisable()
    {
        Player.OnPlayerShooting -= FireWeapons;
        Powerup.OnPowerupGot -= IncreaseWeapons;
        LaserEnemy.OnPlayerDamaged -= DecreaseWeapons;
        Enemy.OnEnemyL1DamagedPlayer -= DecreaseWeapons;
        Enemy.OnBossDamagedPlayer -= DecreaseWeapons;
        EnemiesSpawner.OnBossWaveStarted += SetBossCooldown;
    }

    private void SetBossCooldown()
    {
        shootCooldown = bossShootCooldown;
    }

    protected override void FireWeapons()
    {
        base.FireWeapons();
        
        foreach (var laserPos in weaponsPositions)
        {
            if (laserPos.activeInHierarchy)
            {
                GameObject laser = LaserPool.sharedInstance.GetPooledLaser();

                laser.transform.position = laserPos.transform.position;
                laser.transform.rotation = laserPos.transform.rotation;
                laser.SetActive(true);
            }
        }
    }
}
