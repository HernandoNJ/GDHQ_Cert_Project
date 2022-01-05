using UnityEngine;

public class PlayerWeapons : Weapons
{
    private void OnEnable()
    {
        Player.OnPlayerShooting += ShootLaser;
        Powerup.PowerupGot += IncreaseWeapons;
        LaserEnemy.OnPlayerDamaged += DecreaseWeapons;
        Enemy.OnPlayerDamaged += DecreaseWeapons;
        Enemy.OnBossPlayerDamage += DecreaseWeapons;
    }

    private void OnDisable()
    {
        Player.OnPlayerShooting -= ShootLaser;
        Powerup.PowerupGot -= IncreaseWeapons;
        LaserEnemy.OnPlayerDamaged -= DecreaseWeapons;
        Enemy.OnPlayerDamaged -= DecreaseWeapons;
        Enemy.OnBossPlayerDamage -= DecreaseWeapons;
    }

    private void ShootLaser()
    {
        if (Time.time > timeForNextShoot)
        {
            timeForNextShoot = Time.time + shootCooldown;

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
}
