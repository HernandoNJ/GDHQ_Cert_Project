using UnityEngine;

namespace Training.Patterns.Prototype
{
public class WeaponTester : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Target target;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            currentWeapon.Attack(target);
    }


}
}
