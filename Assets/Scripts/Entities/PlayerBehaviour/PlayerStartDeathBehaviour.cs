using UnityEngine;

public class PlayerStartDeathBehaviour : StateMachineBehaviour
{
    private bool _firstTimeSet = false;

    #region Parameters
    private float _deathTimer;
    private float _deathTime;
    private Player _player;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_firstTimeSet) return;
        _firstTimeSet = true;

        _player = animator.gameObject.GetComponent<Player>();
        _deathTime = _player.GetDeathTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player.IsPlayerPaused) return;

        _deathTimer += Time.deltaTime;

        if (_deathTimer >= _deathTime)
        {
            animator.SetBool("startDeath", false);
            animator.SetBool("isDamaged", false);
            animator.SetBool("isDead", true);
            _player.Death();
        }
    }
}
