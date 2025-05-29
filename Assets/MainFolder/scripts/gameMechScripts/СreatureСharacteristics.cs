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
    public float CurrentHealth
    {
        get { return currentHealth; }
        protected set { currentHealth = value; }
    }
    public bool IsAlive
    {
        get { return isAlive; } // Доступно для чтения
        protected set { isAlive = value; } // Доступно только для наследников
    }
    public virtual void Awake()
    {
        currentHealth = maxHealth;
        HealthBar.fillAmount = currentHealth / maxHealth;
        isAlive = true;
        
    }
    public virtual void Update()
    {
        CheckIsAlive();
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HealthBar.fillAmount = currentHealth / maxHealth;
        Debug.Log($"{this.name} take damage {damage}");

    }
    public virtual void CheckIsAlive()
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
