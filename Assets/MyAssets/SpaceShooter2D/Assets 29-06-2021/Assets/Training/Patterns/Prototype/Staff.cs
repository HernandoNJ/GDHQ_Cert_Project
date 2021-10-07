namespace Training.Patterns.Prototype
{
public class Staff : Weapon
{
    protected override void DoAttack(Target target)
    {
        target.Freeze(5);
        target.TakeDamage(1);
    }
}
}
