using UnityEngine;

public class SimpleEnemyDamageBehaviour : StateMachineBehaviour
{
    private bool _isSetted = false;

    #region Parameters
    private float _inmunityTimer;
    private float _inmunityTime;
    private Enemy _enemy;
    #endregion
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isSetted) return;
        _isSetted = true;
        _enemy = animator.gameObject.GetComponent<Enemy>();

        _inmunityTime = _enemy.GetInmunityTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inmunityTimer += Time.deltaTime;

        if (_inmunityTimer >= _inmunityTime)
        {
            _inmunityTimer = 0; // resets timer
            _enemy.GetIsInmune = false; // can take damage again
            animator.SetBool("isDamaged", false);
        }
    }
}
