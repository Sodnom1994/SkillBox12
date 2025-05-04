using System;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public sealed class PlayerAnimatorController : CreatureAnimatorController
{
    private Animator playerAnimator;
    private SpriteRenderer playerSprite;

    public override Animator �reatureAnimator => playerAnimator;
    public override SpriteRenderer CreatureSprite => playerSprite;

    public override void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        if (playerAnimator == null)
        {
            Debug.LogError("������� ������������ ����������� ��� PlayerAnimatorController!");
        }
    }
    public override void FlipSprite(bool isFacingRight)
    {
        playerSprite.flipX = !playerSprite.flipX;
    }

}
