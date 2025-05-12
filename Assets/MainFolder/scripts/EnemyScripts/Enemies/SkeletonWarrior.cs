using UnityEngine;

public class SkeletonWarrior : EnemyCharacteristics, IAttackable
{
    public CapsuleCollider2D swordCollider;
    public override void Start()
    {
        swordCollider = GetComponentInChildren<CapsuleCollider2D>();
    }
    public override void CheckIsAlive()
    {
        if (currentHealth > 0f)
        {
            base.CheckIsAlive();
        }
        else
        {
            base.CheckIsAlive();
            //NoAnimationDeathHere
            Destroy(gameObject, 1f);
        }
    }
    public void MeleeAttack()
    {
       
    }
}
