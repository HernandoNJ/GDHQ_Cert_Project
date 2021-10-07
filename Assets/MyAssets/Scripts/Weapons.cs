using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private GameObject[] laserPositions;
    [SerializeField] private int weaponsNumber;
    [SerializeField] private float shootCooldown;

    private float timeForNextShoot;

    private static Weapons instance;
    public static Weapons Instance => instance;


    private void Awake() => instance = this;

    private void Start()
    {
        if (instance == null) Debug.LogWarning("PlayerWeapons instance is null");

        foreach (var laserPos in laserPositions) 
        { laserPos.SetActive(false); }

        UpdateActiveWeapons(1);
    }

    private void ActivateWeaponPositions()
    {
        switch (weaponsNumber)
        {
            case 1:
                laserPositions[0].SetActive(true);
                laserPositions[1].SetActive(false);
                laserPositions[2].SetActive(false);
                break;
            case 2:
                laserPositions[0].SetActive(true);
                laserPositions[1].SetActive(true);
                laserPositions[2].SetActive(false);
                break;
            case 3:
                laserPositions[0].SetActive(true);
                laserPositions[1].SetActive(true);
                laserPositions[2].SetActive(true);
                break;
        }
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
    /// <summary> Increase or reduce the number of weapons </summary>
    /// <param name="value"></param>
    public void UpdateActiveWeapons(int value)
    {
        if (weaponsNumber >= 3) return;

        weaponsNumber += value;
        ActivateWeaponPositions();
    }
}
