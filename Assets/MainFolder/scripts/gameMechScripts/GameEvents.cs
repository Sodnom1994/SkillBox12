using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public delegate void PlayerTakeDamageDelegate(float currentHealth);
    public event PlayerTakeDamageDelegate OnPlayerTakeDamage;
    public void RaisePlayerTakeDamage(float currentHealth)
    {
        OnPlayerTakeDamage?.Invoke(currentHealth);
    }
}
