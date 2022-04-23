using UnityEngine;

public class SimpleEnemyChasingBehaviour : StateMachineBehaviour
{
    #region Positions
    private Transform _playerPos;
    private Transform _transform;
    #endregion

    private bool _isSetted = false;

    #region Parameters
    private Enemy _enemy;
    private int _speed;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isSetted) return; // stops re-setting same parameters everytime it starts this state
        _isSetted = true;

        _transform = animator.transform;
        _enemy = animator.gameObject.GetComponent<Enemy>();

        _playerPos = _enemy.TargetPos;
        _speed = _enemy.GetSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _playerPos.position, Time.deltaTime * _speed);
    }
}
