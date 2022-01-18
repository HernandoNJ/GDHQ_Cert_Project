public class MidBossWeapons : Weapons
{
    protected override void SetInitialValues()
    {
        ResetWeaponPositions();
        weaponsIndex = maxWeaponsIndex;
        UpdateWeaponPositions(weaponsIndex);
        ShootWeapons();
    }
    
    private void ShootWeapons()
    {
        // TODO check out 
        //StartCoroutine(ShootingRoutine()); 
    }
    
    // TODO check here and below
    // protected virtual IEnumerator ShootingRoutine()
    // {
    //
    //     //while (gameObject.activeInHierarchy && shootEnabled)
    //     {
    //         //yield return new WaitForSeconds(shootCooldown);
    //         // TODO shoot mechanic: instantiate lasers in firepoint 0
    //     }
    // }
    // protected override IEnumerator ShootingRoutine()
    // {
    //     while (gameObject.activeInHierarchy && playerDestroyed == false)
    //     {
    //         for (float i = 0; i < 0.5f; i += 0.1f)
    //         {
    //             yield return new WaitForSeconds(shootCooldown);
    //             Shoot();
    //         }
    //
    //         yield return new WaitForSeconds(1.5f);
    //     }
    //     
    //     animController.Play("Empty");
    // }
}
