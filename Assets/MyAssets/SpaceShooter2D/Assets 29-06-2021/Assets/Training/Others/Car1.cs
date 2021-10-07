namespace Training.Others
{
    public class Car1 : BaseDamage
    {
        private void Start()
        {
            SetMaxHealth(47);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(3);
        }
    }
}