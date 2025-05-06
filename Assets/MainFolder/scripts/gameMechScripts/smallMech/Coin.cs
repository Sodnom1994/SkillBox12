using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour, ICollectable
{
    private Rigidbody2D coinRigidbody;
    [SerializeField] private float throwForce = 25f;
    [SerializeField] private string animationStateName = "CoinAnimation";
    private Animator animator;
    private void Awake()
    {
        coinRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        AutoDestroy();
        #region Физика разлета монеты и её анимация
        if (coinRigidbody != null)
        {
            Vector2 randomUpSemiSphereDirection = new(
                Random.Range(-0.5f, 0.5f),
                Random.Range(0.5f, 1f)
                );
            coinRigidbody.AddForce(randomUpSemiSphereDirection * throwForce, ForceMode2D.Impulse);
        }
        if (animator != null)
        {
            float animatorLength = GetAnimationLength(animator, animationStateName);
            if (animatorLength > 0f)
            {
                float randomStartTime = Random.Range(0f, animatorLength);
                animator.Play(animationStateName, 0, randomStartTime);
            }
            else
            {
                Debug.LogWarning("Проблемы с длинной анимации монеты");
            }
        }
        else
        {
            Debug.LogWarning("Аниматор монеты ненайден");
        }
    }
    #endregion
    public void Collect()
    {
        //Debug.Log("Монетка поднята");
        gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();

        }
    }
    private float GetAnimationLength(Animator animator, string StateName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == StateName)
            {
                return clip.length;
            }
        }
        return 0f;
    }
    public void AutoDestroy()
    {
        Destroy(gameObject,5f);
    }
}
