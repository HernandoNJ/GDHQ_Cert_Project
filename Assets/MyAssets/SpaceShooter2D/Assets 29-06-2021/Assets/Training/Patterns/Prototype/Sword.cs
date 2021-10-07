namespace Training.Patterns.Prototype
{
public class Sword : Weapon
{
    protected override void DoAttack(Target target)
    {
        target.TakeDamage(5);
    }

    protected override string DamageMessage() { return "slash"; }

}
}
