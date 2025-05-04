using System.Collections;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public sealed class EnemyAnimatorController : CreatureAnimatorController
{
    private Animator enemyAnimator;
    private SpriteRenderer enemySprite;
    public bool resurrectionPlay = false;   
    public override Animator СreatureAnimator => enemyAnimator;
    public override SpriteRenderer CreatureSprite => enemySprite;
    private void Awake()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
    }
    public override void Start()
    {        
        //Debug.Log($"enemySprite = {enemySprite}");
        //Debug.Log($"enemyAnimator = {enemyAnimator}");
        if (enemyAnimator == null || enemySprite == null)
        {
            Debug.LogError("Проверь присваивание компонентов для PlayerAnimatorController!");
        }
    }
    public override void FlipSprite(bool isFacingRight)
    {
        enemySprite.flipX = !enemySprite.flipX;
    }
    public void Resurrection()
    {
        //Debug.Log("Resurrection triggered");
        enemyAnimator.SetTrigger("Resurrection");
        StartCoroutine(ResurrectionFlag());
    }
    IEnumerator ResurrectionFlag()
    {
        yield return new WaitForSeconds(4f);
        resurrectionPlay = true;
    }
}
