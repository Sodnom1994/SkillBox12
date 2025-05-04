using Unity.VisualScripting;
using UnityEngine;

public class Knight : EnemyCharacteristics, IAttackable
{
    //[SerializeField] private float attackRange = 2f;
    public float knightAttackCooldown = 2f;
    public bool isAttacking = false;
    private EnemyAnimatorController knightAnimatorController;
    public BoxCollider2D knightAttackBoxCollider2D;    
    public override void Start()
    {
        knightAttackBoxCollider2D = GetComponentInChildren<BoxCollider2D>();       
        knightAnimatorController = GetComponent<EnemyAnimatorController>();
        knightAnimatorController.Resurrection();
    }
    public override void PatrolAndChase()
    {
        if (isAlive && knightAnimatorController.resurrectionPlay )
        {
            if (!isAttacking)
            {
                base.PatrolAndChase();
            }
        }
    }   
    public void MeleeAttack()
    {
        rb.linearVelocityX = 0f;
        isAttacking = true;
        if (playerTranform == null || knightAnimatorController == null)
        {
            Debug.LogError("playerTransform или knightAnimatorController не инициализированы.");
            return;
        }
        //float distance = Vector2.Distance(playerTranform.position, transform.position);
        //if (distance < attackRange)
        //{
        //    knightAnimatorController.UpdateAttckState();
        //}
        knightAnimatorController.UpdateRunningState(isRunning: false);
        knightAnimatorController.UpdateAttckState();
    }    
       
    public override void VisionColiTransform(float viewDirection)
    {
        Transform transform = visionCollider.transform;
        if (viewDirection > 0 && !facingRight)
        {
            knightAttackBoxCollider2D.transform.localScale = new(-1f, 1f, 1f);
            visionCollider.transform.localPosition = new(-0.052f, 0.45f);
            visionCollider.transform.eulerAngles = new(0, 0, -180f);
        }
        else if (viewDirection < 0 && facingRight)
        {
            knightAttackBoxCollider2D.transform.localScale = new(1f, 1f, 1f);
            visionCollider.transform.localPosition = transform.localPosition;
            visionCollider.transform.eulerAngles = new(0, 0, 0f);
        }
    }
}
