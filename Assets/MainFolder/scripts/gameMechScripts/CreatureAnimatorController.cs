using UnityEngine;
using UnityEngine.Splines;

abstract public class CreatureAnimatorController : MonoBehaviour
{
    public virtual Animator �reatureAnimator { get; }
    public virtual SpriteRenderer CreatureSprite {  get; }
    public virtual void Start()
    {         
        if (�reatureAnimator == null)
        {
            Debug.LogError("������� ������������ ����������� ��� PlayerAnimatorController!");
        }
    }
    public virtual void UpdateRunningState(bool isRunning)
    {
        if (�reatureAnimator != null)
        {
            �reatureAnimator.SetBool("isRunning", isRunning);
        }
    }
    public virtual void FlipSprite(bool isFacingRight)
    {
        
    }
    public virtual void UpdateJumpingState(bool isJumping)
    {
        if (�reatureAnimator != null)
        {
            �reatureAnimator.SetBool("isJumping", isJumping);
        }
    }
    public virtual void UpdateAttckState()
    {
        if (�reatureAnimator != null)
        {
            �reatureAnimator.SetTrigger("isAttacking");
        }
    }
    public virtual void UpdateDeathBool(bool isAlive)
    {
        if(�reatureAnimator != null)
        {
            �reatureAnimator.SetBool("isAlive", !isAlive);
        }
    }
}
