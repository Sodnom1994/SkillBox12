using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyAnimatorController))]
public class EnemyCharacteristics : СreatureСharacteristics, IDropable
{
    #region Объявление переменных 
    [SerializeField] private EnemyAnimatorController enemyAnimatorController;
    public PolygonCollider2D visionCollider;
    [Header("Свойство патруля")]
    public Transform playerTranform;
    [SerializeField] private float patrolDistance = 5.0f;
    [SerializeField] private float patrolSpeed = 0.3f;
    [SerializeField] private float AggroSpeed = 0.4f;
    [SerializeField] private bool isChasing = false;
    [SerializeField] private GameObject visionGameObject;
    [Header("Свойство атаки при колизии")]
    public float enemyDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float nextAttackTime;
    [Space]
    [SerializeField] private float currentTransformX;
    public bool facingRight = true;
    public bool isRunning = false;
    public Image enemyHealthBar;
    public Rigidbody2D rb;
    [Space]
    [Header("Выпадение предметов")]
    [SerializeField] private bool isDroped = false;
    [SerializeField] private GameObject[] loot;
    [SerializeField] private Transform dropPosition;


    public override Image HealthBar => enemyHealthBar;
    #endregion
    #region Присвоение переменных в Start
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Debug.Log($"rb = {rb.name}");
        enemyAnimatorController = GetComponent<EnemyAnimatorController>();
        //Debug.Log($"enemyAnimatorController = {enemyAnimatorController}");
        visionCollider = GetComponentInChildren<PolygonCollider2D>();
        Image[] images = GetComponentsInChildren<Image>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTranform = player.transform;
        currentTransformX = transform.position.x;
        foreach (Image image in images)
        {
            if (image.name == "EnemyHealthBar")
                enemyHealthBar = image;
            break;
        }
        if (rb == null || enemyAnimatorController == null)
        {
            Debug.LogError("Проверь присваивание компонентов для EnemyСharacteristics!");
        }
    }
    #endregion
    public override void Update()
    {
        base.Update();
        if (isAlive)
        {
            PatrolAndChase();
        }
        else
        {
            PatrolStop();
            Debug.Log("Enemy die");
        }
    }
    public override void CheckisAlive()
    {
        if (currentHealth > 0f)
        {
            isAlive = true;
        }
        else
        {
            //Debug.Log($"Using deathAnimation");
            isRunning = false;
            isAlive = false;
            enemyAnimatorController.UpdateDeathBool(isAlive);
            if (!isDroped)
            {
                DropLoot();
                isDroped = true;
            }
            Destroy(gameObject, 3f);
        }
    }
    #region Выпадение предметов из врага
    public void DropLoot()
    {
        if (loot != null && loot.Length != 0)
        {
            int randomIndex = Random.Range(0, loot.Length);
            GameObject lootPrefab = loot[randomIndex];
            if (lootPrefab != null)
            {
                Instantiate(lootPrefab, dropPosition.position, Quaternion.identity);
                Debug.Log($"Выпало: {lootPrefab.name}");
            }
        }
        else
        {
            Debug.Log("Нет лута для выпадения");
        }
    }

    #endregion
    #region Урон при соприкосновении игрока с врагом
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (collision.gameObject.TryGetComponent(out СreatureСharacteristics characteristics))
        {
            if (Time.time >= nextAttackTime)
            {
                characteristics.TakeDamage(enemyDamage);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
    #endregion
    #region Патруль и преследование
    public virtual void PatrolAndChase()
    {
        if (!isChasing)
        {
            Patrol();
        }
        else
        {
            Debug.Log("EnemyChasingPlayer");
            PatrolStop();
            ChasePlayer();
        }
    }

    public void Patrol()
    {
        rb.linearVelocityX = patrolSpeed;
        float direction = rb.linearVelocityX;
        isRunning = direction != 0f;
        enemyAnimatorController.UpdateRunningState(isRunning);
        // Debug.Log($"viewDirection = {Direction}");
        // Debug.Log($"facingRight = {facingRight}");
        if (Mathf.Abs(transform.position.x - currentTransformX) >= patrolDistance)
        {
            currentTransformX = transform.position.x;
            // Инвертируем направление
            patrolSpeed *= -1;
        }
        VisionColiTransform(direction);
        // Поворачиваем спрайт в зависимости от направления движения
        FlipSprite(direction);
    }
    public void PlayerDetected()
    {
        //Debug.Log($"EnemyCharacteristics playerDetected");
        //метод нужен для поднятия флага при колизии в скрипте 
        //EnemyVision что игрок вошел в зону "видимости"(колайдер)
        isChasing = true;
    }
    private void ChasePlayer()
    {
        if (playerTranform != null)
        {
            Vector3 direction = (playerTranform.transform.position - transform.position).normalized;
            rb.linearVelocityX = direction.x * AggroSpeed;
        }
        else
        {
            return;
        }

    }
    private void FlipSprite(float direction)
    {
        if ((direction > 0 && facingRight) || (direction < 0 && !facingRight))
        {
            facingRight = !facingRight;
            enemyAnimatorController.FlipSprite(facingRight);
        }
    }
    private void PatrolStop()
    {
        rb.linearVelocityX = 0;
    }
    public virtual void VisionColiTransform(float viewDirection)
    {
        if (viewDirection > 0 && !facingRight)
        {
            visionCollider.transform.localPosition = new(0f, 0f, 0f);
            visionCollider.transform.eulerAngles = new(0, 0, 0f);
        }
        else if (viewDirection < 0 && facingRight)
        {
            //Debug.Log("!");
            visionCollider.transform.localPosition = new(-0.052f, 0.5f);
            visionCollider.transform.eulerAngles = new(0, 0, -180f);
        }
    }
    #endregion
}
