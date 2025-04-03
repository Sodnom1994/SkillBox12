using UnityEngine;

public class EnemyAnimatorController : CreatureAnimatorController
{
    private Animator enemyAnimator;
    public override Animator СreatureAnimator => enemyAnimator;
    public override void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();        
        if (enemyAnimator == null)
        {
            Debug.LogError("Проверь присваивание компонентов для PlayerAnimatorController!");
        }
    }
}
