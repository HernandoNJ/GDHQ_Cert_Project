using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] protected GameObject[] weaponsPositions;
    [SerializeField] protected int weaponsAmount;
    [SerializeField] protected int maxWeapons;
    [SerializeField] protected float shootCooldown;

    protected float timeForNextShoot;

    protected void Start()
    {
        DisableWeaponPositions();
        weaponsAmount = 1;
        UpdateWeaponPositions();
    }

    private void DisableWeaponPositions()
    {
        foreach (var laserPos in weaponsPositions) laserPos.SetActive(false);
    }

    protected void IncreaseWeapons()
    {
        weaponsAmount++;
        if (weaponsAmount > maxWeapons)
        {
            weaponsAmount = maxWeapons;
            return;
        }
        
        UpdateWeaponPositions();
    }

    protected void DecreaseWeapons()
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
                weaponsPositions[0].SetActive(true);
                weaponsPositions[1].SetActive(false);
                weaponsPositions[2].SetActive(false);
                break;
            case 2:
                weaponsPositions[0].SetActive(false);
                weaponsPositions[1].SetActive(true);
                weaponsPositions[2].SetActive(true);
                shootCooldown = 0.1f;
                break;
            case 3:
                weaponsPositions[0].SetActive(true);
                weaponsPositions[1].SetActive(true);
                weaponsPositions[2].SetActive(true);
                shootCooldown = 0.5f;
                break;
        }

        Debug.Log($"weapons number updated. weapons number: {weaponsAmount}");
    }
}