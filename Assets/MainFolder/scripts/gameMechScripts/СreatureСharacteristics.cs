using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class СreatureСharacteristics : MonoBehaviour
{
    [Header("Свойство здоровья")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    public virtual Image HealthBar { get; }
    protected bool isAlive;

    public bool IsAlive
    {
        get { return isAlive; } // Доступно для чтения
        protected set { isAlive = value; } // Доступно только для наследников
    }
    public void Awake()
    {
        currentHealth = maxHealth;
        isAlive = true;
        
    }
    public virtual void Update()
    {
        CheckisAlive();
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthBar.fillAmount = currentHealth / maxHealth;
        CheckisAlive();
    }
    public virtual void CheckisAlive()
    {
        if (currentHealth > 0f)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
            
        }
    }

}
