using UnityEngine;
using static UnityEngine.Debug;

namespace Training.Patterns.Prototype
{
public class Weaponv2 : MonoBehaviour
{
    [SerializeField] private WeaponDataTraining weaponData;
    [SerializeField] private Transform weaponModelTransformParent;
    private GameObject model;

    private void OnEnable()
    {
        if (model != null) Destroy(model);

        // Set model from WeaponData
        if (weaponData.Model != null)
        {
            model = Instantiate(weaponData.Model);
            model.transform.SetParent(weaponModelTransformParent, false);
        }
    }

    public void Attack(Target target)
    {
        // Additional code can be added in each case
        // For example, adding particle effects

        if (weaponData.Damage > 0)
            target.TakeDamage(weaponData.Damage);

        // if(weaponData.StuntDuration > 0)
        //     target.Stunt(weaponData.StuntDuration);

        if (weaponData.FreezeDuration > 0)
            target.Freeze(weaponData.FreezeDuration);

        string message = string.IsNullOrEmpty(weaponData.Message) ? "hit" : weaponData.Message;

        Log("You " + message + " - " + target.name);

    }

}
}
