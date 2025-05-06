using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour, ICollectable
{
    private Rigidbody2D coinRigidbody;
    [SerializeField] private float throwForce = 1f;
    private void Awake()
    {
        coinRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (coinRigidbody != null)
        {
            Vector2 randomUpSemiSphereDirection = new(
                Random.Range(-0.5f, 0.5f),
                Random.Range(0.5f, 1f)
                );
            coinRigidbody.AddForce(randomUpSemiSphereDirection * throwForce, ForceMode2D.Impulse);
        }
    }
    public void Collect()
    {
        Debug.Log("Монетка поднята");
        gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();

        }
    }
}
