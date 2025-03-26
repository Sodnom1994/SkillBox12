using UnityEngine;

abstract public class CreatureAnimatorController : MonoBehaviour
{
    public virtual Animator ÑreatureAnimator { get; }
    public virtual SpriteRenderer CreatureSprite {  get; }
    public virtual void Start()
    {         
        if (ÑreatureAnimator == null)
        {
            Debug.LogError("Ïğîâåğü ïğèñâàèâàíèå êîìïîíåíòîâ äëÿ PlayerAnimatorController!");
        }
    }
    public virtual void UpdateRunningState(bool isRunning)
    {
        if (ÑreatureAnimator != null)
        {
            ÑreatureAnimator.SetBool("isRunning", isRunning);
        }
    }
    public virtual void FlipSprite(bool isFacingRight)
    {
        
    }
    public virtual void UpdateJumpingState(bool isJumping)
    {
        if (ÑreatureAnimator != null)
        {
            ÑreatureAnimator.SetBool("isJumping", isJumping);
        }
    }
}
