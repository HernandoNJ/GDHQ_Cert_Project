namespace Training.Patterns.Prototype
{
public class MegaSword : Weapon
{
    protected override void DoAttack(Target target)
    {
        target.TakeDamage(15);
        int rand = UnityEngine.Random.Range(0, 100);

        if (rand <= 30) // 30% probability (rand <= 30)
            // target.Stun(2);
            print("rand value: " + rand);
    }
}
}