using UnityEngine;

public class PlayerDamagedBehaviour : StateMachineBehaviour
{
    private bool _firstTimeSet = false;

    #region Parameters
    private float _inmunityTimer;
    private float _inmunityTime;
    private Player _player;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_firstTimeSet) return;
        _firstTimeSet = true;

        _player = animator.gameObject.GetComponent<Player>();

        _inmunityTime = _player.GetInmunityTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player.IsPlayerPaused) return;

        _inmunityTimer += Time.deltaTime;

        if (_inmunityTimer >= _inmunityTime)
        {
            _inmunityTimer = 0;
            _player.IsInmune = false;
            animator.SetBool("isDamaged", false);
        }
    }
}
