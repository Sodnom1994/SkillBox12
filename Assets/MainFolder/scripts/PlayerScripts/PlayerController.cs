using Unity.Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Параметры скорости передвижения и силы прыжка Игрока
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private AnimationCurve movementCurve;
    //Для проверки что слой взаимодействия является "нужной" землей
    [Header("Ground Check")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Rigidbody2D rb;
    public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [Header("Attack Settings")]
    public float attackDamage;
    [SerializeField] private float nextAttackTime;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform attackPosition;
    public Vector2 attackDirecton;

    [Header("Animator Settings")]
    public bool facingRight = true;
    [SerializeField] private PlayerAnimatorController playerAnimatorController;
    [SerializeField] private PlayerСharacteristics playerСharacteristics;


    void Start()
    {

        //Присваиваем для rb и playerAnimatorController <= RigidBody2D и playerAnimatorController из GameObject PlayerParametrs
        playerСharacteristics = GetComponent<PlayerСharacteristics>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        if (rb == null || playerAnimatorController == null)
        {
            Debug.LogError("Проверь присваивание компонентов для PlayerController!");
        }
    }
    void Update()
    {
        #region Передвижение игрока
        //Проверка персонажа на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //Управление перемещением персонажа
        float horizontalInput = Input.GetAxis("Horizontal");
        if (playerСharacteristics.IsAlive)
        {
            if (isGrounded)
            {
                Move(horizontalInput);
            }
            //Управление прыжком персонажа
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
            #endregion
            #region Атака и анимация атаки игрока
            if (Input.GetButton("Attack") && Time.time >= nextAttackTime)
            {
                Attack();
                playerAnimatorController.UpdateAttckState();
                nextAttackTime = Time.time + attackCooldown;
            }
            #endregion
            #region Управление анимациями игрока

            //Переключение анимации на бег если horizontalInput !=0
            playerAnimatorController.UpdateRunningState(Mathf.Abs(horizontalInput) > 0.2);

            //Поворт спрайта в направлении движения
            if (isGrounded && ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight)))
            {
                facingRight = !facingRight;
                playerAnimatorController.FlipSprite(facingRight);
            }
            //Переключение bool "isJumping" для аниматора если персонаж не на земле
            playerAnimatorController.UpdateJumpingState(!isGrounded);
            #endregion
        }

        playerСharacteristics.Update();

    }
    #region Методы для передвижения игрока
    private void Move(float horizontalInput)
    {
        Vector2 movement = new(movementCurve.Evaluate(horizontalInput * moveSpeed), rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    #endregion    
    private void Attack()
    {
        GameObject projectile = Instantiate(projectilePrefab, attackPosition.position, Quaternion.identity);
        if (projectile.TryGetComponent<ProjectaileScript>(out var projectileScript))
        {
            Vector2 direction = facingRight ? Vector2.right : Vector2.left;
            projectileScript.SetDamage(attackDamage);
            projectileScript.SetDirection(direction);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //"Сканер" соприкосновения с землей в радиусе groundCheckRadius для эдитора
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
#endif
}
