using UnityEngine;

public class EnemyChasingBehaviour : StateMachineBehaviour
{
    #region Positions
    private Transform _playerPos;
    private Transform _transform;
    #endregion

    #region Parameters
    private Enemy _enemy;
    private int _speed;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform = animator.transform;
        _enemy = animator.gameObject.GetComponent<Enemy>();

        _playerPos = _enemy.TargetPos;
        _speed = _enemy.GetSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _playerPos.position, Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
