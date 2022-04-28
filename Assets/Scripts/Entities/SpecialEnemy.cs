using UnityEngine;

public class SpecialEnemy : Enemy
{
    [SerializeField] private float _tpDistance = 20;
    private bool _canDodge = true;

    public override void TakeDamage(int value, int knockback)
    {
        if (_canDodge)
        {
            TeleportsBehindYou();
            return;
        }

        _animator.SetBool("isDamaged", true);
        base.TakeDamage(value, 0);
    }

    private void TeleportsBehindYou()
    {
        // tips fedora
        Vector3 direction = transform.position - _playerPos.position;
        transform.position = direction.normalized * -_tpDistance; // moves enemy behind player 

        _canDodge = false;
    }

    public override void Death()
    {
        _canDodge = true;
        base.Death();
    }
}
