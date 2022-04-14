using UnityEngine;
using TMPro;

public class BouncingBall : MonoBehaviour
{
    #region Score
    [SerializeField] private TMP_Text _scoreCounter;
    private int _scoreValue;
    #endregion
    private Camera _cam;
    [SerializeField] private float _impulseForce = 1000;
    [SerializeField] private LayerMask _ballLayer;
    [SerializeField] private GameObject _ball;
    private float _forceMultiplier = 400;

    private void Start()
    {
        _cam = Camera.main;
        _ball.GetComponent<Rigidbody>().AddForce(Vector3.up * _impulseForce, ForceMode.Impulse);
        _scoreValue = 0;
        _scoreCounter.text = _scoreValue.ToString();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, _ballLayer))
        {
            if (!hit.transform.CompareTag("Ball")) return;

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();

                // calculate direction
                Vector3 _forceDirection = hit.transform.position - hit.point;
                _forceDirection = _forceDirection.normalized;

                // add force
                if (rb.velocity.x <= 0)
                {
                    rb.velocity = Vector3.zero;
                }
                rb.AddForce(_forceDirection * _impulseForce, ForceMode.Impulse);

                UpdateScore();
                _impulseForce += _forceMultiplier;

                _ball.transform.localScale = _ball.transform.localScale - new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    private void UpdateScore()
    {
        _scoreValue += 1;
        _scoreCounter.text = _scoreValue.ToString();
    }

    public void Die()
    {
        _ball.transform.localScale = new Vector3(15, 15, 15);
        _scoreValue = 0;
        _scoreCounter.text = _scoreValue.ToString();
    }
}
