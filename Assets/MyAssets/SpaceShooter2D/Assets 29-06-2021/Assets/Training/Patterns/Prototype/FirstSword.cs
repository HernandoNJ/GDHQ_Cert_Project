using UnityEngine;
using static UnityEngine.Debug;

namespace Training.Patterns.Prototype
{
public class FirstSword : MonoBehaviour
{
    public int damage = 1;
    public void Attack(Target target)
    {
        target.TakeDamage(damage);
        Log("Damage applied to : -- " + target.name);
    }

}
}
