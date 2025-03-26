using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Параметры скорости передвижения и силы прыжка Игрока
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;

    //Для проверки что слой взаимодействия является "нужной" землей
    [Header("Ground Check")]    
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;
    private PlayerAnimatorController playerAnimatorController;

    void Start()
    {       
        //Присваиваем для rb и playerAnimatorController <= RigidBody2D и playerAnimatorController из GameObject PlayerParametrs
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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if(isGrounded)
        {
            Move(horizontalInput);
        }
        //Управление прыжком персонажа
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        #endregion

        #region Управление анимациями игрока

        //Переключение анимации на бег если horizontalInput !=0
        playerAnimatorController.UpdateRunningState(horizontalInput != 0);

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
    #region Методы для передвижения игрока
    private void Move(float horizontalInput)
    {
        Vector2 movement = new(horizontalInput * moveSpeed, rb.linearVelocity.y);

        rb.linearVelocity = movement;
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    #endregion

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
