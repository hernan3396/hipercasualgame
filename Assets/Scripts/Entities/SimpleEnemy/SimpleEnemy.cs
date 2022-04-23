using UnityEngine;

public class SimpleEnemy : Enemy
{
    public override void TakeDamage(int value, int knockback)
    {
        if (_isInmune) return; // esta en base, pero por lo visto es necesario aca tambien (?)

        base.TakeDamage(value, knockback);
        Knockback(knockback);
        _animator.SetBool("isDamaged", true);
    }

    private void Knockback(int knockback)
    {
        Vector3 direction = transform.position - _playerPos.position;
        _rb.AddForce(direction.normalized * knockback, ForceMode.Impulse);
    }
}
