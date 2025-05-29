using UnityEngine;

public class TakeDamagefromObstacles : MonoBehaviour
{
    [SerializeField] private float nextAttackTime = 3.0f;
    [SerializeField] private float enemyDamage = 15.0f;
    [SerializeField] private float attackCooldown = 2.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player deteced");
            if (collision.gameObject.TryGetComponent(out ÑreatureÑharacteristics characteristics))
            {
                Debug.Log($"characteristics detected{characteristics}");
                if (Time.time >= nextAttackTime)
                {
                    characteristics.TakeDamage(enemyDamage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }
}
