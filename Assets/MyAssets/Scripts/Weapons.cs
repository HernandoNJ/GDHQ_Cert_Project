using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] protected GameObject[] weaponsPositions;
    [SerializeField] protected List<GameObject> weaponsPrefabs;
    [SerializeField] protected int weaponsIndex;
    [SerializeField] protected int minWeaponsIndex;
    [SerializeField] protected int maxWeaponsIndex;
    [SerializeField] protected float shootCooldown;
    [SerializeField] protected float timeForNextShoot;

    protected virtual void Start()
    {
        DisableWeaponPositions();
        minWeaponsIndex = 0;
        maxWeaponsIndex = 2;
        weaponsIndex = minWeaponsIndex;
        UpdateWeaponPositions(weaponsIndex);
    }

    private void DisableWeaponPositions()
    {
        foreach (var laserPos in weaponsPositions) laserPos.SetActive(false);
    }

    protected void IncreaseWeapons(int value)
    {
        weaponsIndex += value;

        if (weaponsIndex > maxWeaponsIndex)
        {
            weaponsIndex = maxWeaponsIndex;
            Debug.Log("weapons added but max value exceeded");
            return;
        }

        UpdateWeaponPositions(weaponsIndex);
    }

    protected void DecreaseWeapons(int value)
    {
        weaponsIndex -= value;

        if (weaponsIndex < minWeaponsIndex)
        {
            weaponsIndex = minWeaponsIndex;
            Debug.Log("weapons added but wrong min value");
            return;
        }

        UpdateWeaponPositions(weaponsIndex);
    }

    protected void UpdateWeaponPositions(int indexArg)
    {
        switch (indexArg)
        {
            case 0:
                weaponsPositions[0].SetActive(true);
                weaponsPositions[1].SetActive(false);
                weaponsPositions[2].SetActive(false);
                break;
            case 1:
                weaponsPositions[0].SetActive(false);
                weaponsPositions[1].SetActive(true);
                weaponsPositions[2].SetActive(true);
                break;
            case 2:
                weaponsPositions[0].SetActive(true);
                weaponsPositions[1].SetActive(true);
                weaponsPositions[2].SetActive(true);
                break;
        }
    }

    protected void SetCooldown()
    {
        if (Time.time > timeForNextShoot) { timeForNextShoot = Time.time + shootCooldown; }
    }

    protected virtual void FireWeapons()
    {
        SetCooldown();
    }
}
