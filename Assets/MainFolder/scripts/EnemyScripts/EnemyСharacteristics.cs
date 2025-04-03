using UnityEngine;
using UnityEngine.UI;

public class EnemyСharacteristics : СreatureСharacteristics
{

    private EnemyAnimatorController enemyController;
    private Rigidbody2D rb;
    [Header("Свойство атаки")]
    [SerializeField] private float enemyDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float nextAttackTime;
    public Image enemyHealthBar;
    public override Image HealthBar => enemyHealthBar;


    public void Start()
    {
        Image[] images = GetComponentsInChildren<Image>();
        if (images.Length > 0)
        {            
            Image firstImage = images[1];//тут я знаю какой по индексу идет image с зеленым баром
            enemyHealthBar = firstImage;            
        }
        else
        {
            Debug.LogWarning("Не найдено ни одного Image!");
        }
        rb = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyAnimatorController>();
        
        if (rb == null || enemyController == null)
        {
            Debug.LogError("Проверь присваивание компонентов для EnemyСharacteristics!");
        }
    }
    public override void Update()
    {
        base.Update();

    }
    public override void CheckisAlive()
    {
        if (currentHealth > 0f)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
            enemyController.UpdateDeathBool(!isAlive);
            Destroy(gameObject, 1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent(out СreatureСharacteristics characteristics);
            if (characteristics != null)
            {
                if (Time.time >= nextAttackTime)
                {
                    characteristics.TakeDamage(enemyDamage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }
}
