using UnityEngine;

public class PlayerWeapons : Weapons
{
    [SerializeField] private float fastShooting;
    [SerializeField] private float fastestShooting;
    
    // todo check why lasers are destroyed
    
    private void OnEnable()
    {
        Player.OnPlayerShooting += FireWeapons;
        Player.OnAddWeapons += IncreaseWeapons;
        Player.OnReduceWeapons += DecreaseWeapons;
        LaserEnemy.EnemyLaserDamagedPlayer += DecreaseWeapons;
        Enemy.OnEnemyL1DamagedPlayer += DecreaseWeapons;
        Enemy.OnMidOrFinalBossDamagedPlayer += DecreaseWeapons;
        EnemiesSpawner.OnMidBossWaveStarted += SetMidBossCooldown;
    }

    private void OnDisable()
    {
        Player.OnPlayerShooting -= FireWeapons;
        Player.OnAddWeapons -= IncreaseWeapons;
        Player.OnReduceWeapons -= DecreaseWeapons;
        LaserEnemy.EnemyLaserDamagedPlayer -= DecreaseWeapons;
        Enemy.OnEnemyL1DamagedPlayer -= DecreaseWeapons;
        Enemy.OnMidOrFinalBossDamagedPlayer -= DecreaseWeapons;
        EnemiesSpawner.OnMidBossWaveStarted += SetMidBossCooldown;
    }

    private void SetMidBossCooldown() => shootCooldown = fastShooting;
    private void FinalBossCooldown() => shootCooldown = fastestShooting;

    protected override void FireWeapons()
    {
        base.FireWeapons();
        
        foreach (var laserPos in weaponsPositions)
        {
            if (laserPos.activeInHierarchy)
            {
                GameObject laser = LaserPool.instance.GetPooledLaser();

                laser.transform.position = laserPos.transform.position;
                laser.transform.rotation = laserPos.transform.rotation;
                laser.SetActive(true);
            }
        }
    }
}
