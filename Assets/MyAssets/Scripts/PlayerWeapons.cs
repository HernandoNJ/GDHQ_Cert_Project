using UnityEngine;

public class PlayerWeapons : Weapons
{
    [SerializeField] private float bossShootCooldown;
    
    private void OnEnable()
    {
        Player.OnPlayerShooting += FireWeapons;
        Player.OnAddWeapons += IncreaseWeapons;
        Player.OnReduceWeapons += DecreaseWeapons;
        LaserEnemy.OnPlayerDamaged += DecreaseWeapons;
        Enemy.OnEnemyL1DamagedPlayer += DecreaseWeapons;
        Enemy.OnMidOrFinalBossDamagedPlayer += DecreaseWeapons;
        EnemiesSpawner.OnBossWaveStarted += SetBossCooldown;
    }

    private void ReduceWeapons(int obj)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        Player.OnPlayerShooting -= FireWeapons;
        Player.OnAddWeapons -= IncreaseWeapons;
        Player.OnReduceWeapons -= DecreaseWeapons;
        LaserEnemy.OnPlayerDamaged -= DecreaseWeapons;
        Enemy.OnEnemyL1DamagedPlayer -= DecreaseWeapons;
        Enemy.OnMidOrFinalBossDamagedPlayer -= DecreaseWeapons;
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
