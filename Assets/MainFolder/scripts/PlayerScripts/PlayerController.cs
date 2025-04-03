using Unity.Cinemachine;
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
    [SerializeField] private Player�haracteristics player�haracteristics;


    void Start()
    {

        //����������� ��� rb � playerAnimatorController <= RigidBody2D � playerAnimatorController �� GameObject PlayerParametrs
        player�haracteristics = GetComponent<Player�haracteristics>();
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
        if (player�haracteristics.IsAlive)
        {
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
            #region ����� � �������� ����� ������
            if (Input.GetButton("Attack") && Time.time >= nextAttackTime)
            {
                Attack();
                playerAnimatorController.UpdateAttckState();
                nextAttackTime = Time.time + attackCooldown;
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

        player�haracteristics.Update();

    }
    #region ������ ��� ������������ ������
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
        //"������" ��������������� � ������ � ������� groundCheckRadius ��� �������
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
#endif
}
