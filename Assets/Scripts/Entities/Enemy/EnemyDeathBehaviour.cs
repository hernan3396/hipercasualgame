using UnityEngine;

public class EnemyDeathBehaviour : StateMachineBehaviour
{
    private bool _isSetted = false;

    #region Parameters
    private float _deathTimer;
    private float _deathTime;
    private Enemy _enemy;
    #endregion
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isSetted) return;
        _isSetted = true;

        _enemy = animator.gameObject.GetComponent<Enemy>();

        _deathTime = _enemy.GetDeathTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _deathTimer += Time.deltaTime;

        if (_deathTimer >= _deathTime)
        {
            animator.SetBool("isDead", false);
            animator.SetBool("isDamaged", false);
            _enemy.Death();
        }
    }
}
