using UnityEngine;

namespace Training.Patterns.Prototype
{
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/Inventory/WeaponDataTraining")]
public class WeaponDataTraining : ScriptableObject
{
    /*
    Really useful if designers use the Unity editor frequently

    This class can be made as a non scriptableobject
    For example, for a data service
    It can be serialized to JSON or XML and be loaded in
    In that case we don't have to worry about builds at all
    Because it can be completely changed or reloaded
    Without any engineering at all
    */

    public int Damage;
    public string Message;

    // Model can be get just here, nowhere elese 
    // weapon's image, mesh ==> Mace00_mesh
    public GameObject Model;
    public int StuntDuration;
    public int FreezeDuration;

    // A way to Get the Model in Weaponv2
    // Look for its name and instantiate it
    // instead of getting a reference
    public string ModelName;


}
}