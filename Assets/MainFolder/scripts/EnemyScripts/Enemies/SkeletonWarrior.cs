using UnityEngine;

public class SkeletonWarrior : EnemyCharacteristics, IAttackable
{
    public CapsuleCollider2D swordCollider;
    public override void Start()
    {
        swordCollider = GetComponentInChildren<CapsuleCollider2D>();
    }
    public override void CheckisAlive()
    {
        if (currentHealth > 0f)
        {
            base.CheckisAlive();
        }
        else
        {
            base.CheckisAlive();
            //NoAnimationDeathHere
            Destroy(gameObject, 1f);
        }
    }
    public void MeleeAttack()
    {
       
    }
}
