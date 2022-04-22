using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    private UIController _uiController;
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        _uiController = GameManager.GetInstance.GetUIController;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _uiController.UpdateLifes(_currentHealth);

        if (_currentHealth <= 0)
            Death();
    }

    private void Death()
    {
        gameObject.SetActive(false);
        _uiController.DeathScreen();
    }
}
