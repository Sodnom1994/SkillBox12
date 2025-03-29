using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //��������� �������� ������������ � ���� ������ ������
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private AnimationCurve movementCurve;

    //��� �������� ��� ���� �������������� �������� "������" ������
    [Header("Ground Check")]
    public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;

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
        float horizontalInput = Input.GetAxis("Horizontal");

        if (isGrounded)
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
        playerAnimatorController.UpdateRunningState(Mathf.Abs(horizontalInput) > 0.2);

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
        Vector2 movement = new(movementCurve.Evaluate(horizontalInput*moveSpeed), rb.linearVelocity.y);

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
