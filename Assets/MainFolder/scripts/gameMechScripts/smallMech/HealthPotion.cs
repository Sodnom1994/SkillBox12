using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class HealthPotion : MonoBehaviour, ICollectable
{
    [SerializeField] private float healAmount = 20.0f;
    [SerializeField] private float throwForce = 25f;
    [SerializeField] private Rigidbody2D hpPotionrb;
    private void Awake()
    {
        hpPotionrb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (hpPotionrb != null)
        {
            Vector2 randomUpSemiSphereDirection = new(
                 Random.Range(-0.5f, 0.5f),
                 Random.Range(0.5f, 1f)
                 );
            hpPotionrb.AddForce(randomUpSemiSphereDirection * throwForce, ForceMode2D.Impulse);
        }
    }
    public void Collect()
    {
        //Debug.Log("Çåëüå ïîäîáğàíî");
        if (PlayerÑharacteristics.Instance != null)
        {
            PlayerÑharacteristics.Instance.Heal(healAmount);
            Debug.Log($"Èãğîê âîññòàíîâèë {healAmount} çäîğîâüÿ");
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerÑharacteristics.Instance != null)
            {
                Collect();
            }
            Destroy(gameObject);
        }
    }
}
