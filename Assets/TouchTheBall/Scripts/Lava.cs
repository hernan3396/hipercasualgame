using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private BouncingBall _bouncingBall;
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _bouncingBall.Die();
        }
    }
}
