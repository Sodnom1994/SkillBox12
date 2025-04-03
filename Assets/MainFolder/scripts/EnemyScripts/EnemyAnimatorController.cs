using UnityEngine;

public class EnemyAnimatorController : CreatureAnimatorController
{
    private Animator enemyAnimator;
    public override Animator �reatureAnimator => enemyAnimator;
    public override void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();        
        if (enemyAnimator == null)
        {
            Debug.LogError("������� ������������ ����������� ��� PlayerAnimatorController!");
        }
    }
}
