public class SimpleEnemy : Enemy
{
    public override void TakeDamage(int value)
    {
        if (_isInmune) return; // esta en base, pero por lo visto es necesario aca tambien (?)
        base.TakeDamage(value);
        _animator.SetBool("isDamaged", true);
    }
}
