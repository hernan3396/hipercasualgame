using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private string objective;
    public UnityEvent onEnter;
    public UnityEvent onExit;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(objective))
            onEnter?.Invoke();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(objective))
            onExit?.Invoke();
    }
}