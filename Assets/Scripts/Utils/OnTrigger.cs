using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private string objective;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(objective))
            onEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(objective))
            onExit?.Invoke();
    }
}