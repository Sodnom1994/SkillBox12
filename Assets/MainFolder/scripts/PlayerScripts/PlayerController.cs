using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //��������� �������� ������������ � ���� ������ ������
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;

    //��� �������� ��� ���� �������������� �������� "������" ������
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
        //����������� ��� rb � playerAnimatorController <= RigidBody2D � playerAnimatorController �� GameObject PlayerParametrs
        rb = GetComponent<Rigidbody2D>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        if (rb == null || playerAnimatorController == null)
        {
            Debug.LogError("������� ������������ ����������� ��� PlayerController!");
        }
        
    }
    void Update()
    {
        #region ������������ ������
        //�������� ��������� �� �����
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //���������� ������������ ���������
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if(isGrounded)
        {
            Move(horizontalInput);
        }
        //���������� ������� ���������
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        #endregion

        #region ���������� ���������� ������

        //������������ �������� �� ��� ���� horizontalInput !=0
        playerAnimatorController.UpdateRunningState(horizontalInput != 0);

        //������ ������� � ����������� ��������
        if (isGrounded && ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight)))
        {
            facingRight = !facingRight;
            playerAnimatorController.FlipSprite(facingRight);
            
        }
        //������������ bool "isJumping" ��� ��������� ���� �������� �� �� �����
        playerAnimatorController.UpdateJumpingState(!isGrounded);
        #endregion
    }
    #region ������ ��� ������������ ������
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
        //"������" ��������������� � ������ � ������� groundCheckRadius ��� �������
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
#endif
}
