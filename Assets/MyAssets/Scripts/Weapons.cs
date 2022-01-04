using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private GameObject[] laserPositions;
    [SerializeField] private int weaponsAmount;
    [SerializeField] private int maxWeapons;
    [SerializeField] private float shootCooldown;

    private float timeForNextShoot;

    private static Weapons instance;
    public static Weapons Instance => instance;

    private void Awake() => instance = this;

    private void OnEnable()
    {
        Powerup.PowerupGot += IncreaseWeapons;
        LaserEnemy.OnPlayerDamaged += DecreaseWeapons;
        Enemy.OnPlayerDamaged += DecreaseWeapons;
        Enemy.OnBossPlayerDamage += DecreaseWeapons;
    }

    private void OnDisable()
    {
        Powerup.PowerupGot -= IncreaseWeapons;
        LaserEnemy.OnPlayerDamaged -= DecreaseWeapons;
        Enemy.OnPlayerDamaged -= DecreaseWeapons;
        Enemy.OnBossPlayerDamage -= DecreaseWeapons;
    }

    private void Start()
    {
        CheckIfWeaponsNull();
        DisableWeaponPositions();
        weaponsAmount = 1;
        UpdateWeaponPositions();
    }

    private void CheckIfWeaponsNull()
    {
        if (instance == null) Debug.LogWarning("PlayerWeapons is null");
    }

    private void DisableWeaponPositions()
    {
        foreach (var laserPos in laserPositions) laserPos.SetActive(false);
    }

    private void IncreaseWeapons()
    {
        weaponsAmount++;
        if (weaponsAmount > maxWeapons)
        {
            weaponsAmount = maxWeapons;
            return;
        }
        
        UpdateWeaponPositions();
    }

    private void DecreaseWeapons()
    {
        weaponsAmount--;
        if (weaponsAmount < 1)
        {
            weaponsAmount = 1;
            return;
        }
        
        UpdateWeaponPositions();
    }

    private void UpdateWeaponPositions()
    {
        switch (weaponsAmount)
        {
            case 1:
                laserPositions[0].SetActive(true);
                laserPositions[1].SetActive(false);
                laserPositions[2].SetActive(false);
                break;
            case 2:
                laserPositions[0].SetActive(false);
                laserPositions[1].SetActive(true);
                laserPositions[2].SetActive(true);
                shootCooldown = 0.1f;
                break;
            case 3:
                laserPositions[0].SetActive(true);
                laserPositions[1].SetActive(true);
                laserPositions[2].SetActive(true);
                shootCooldown = 0.5f;
                break;
        }

        Debug.Log($"weapons number updated. weapons number: {weaponsAmount}");
    }

    public void ShootLaser()
    {
        if (Time.time > timeForNextShoot)
        {
            timeForNextShoot = Time.time + shootCooldown;

            foreach (var laserPos in laserPositions)
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