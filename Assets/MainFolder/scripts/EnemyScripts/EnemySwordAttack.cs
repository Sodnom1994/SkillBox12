using UnityEngine;

public class EnemySwordAttack : MonoBehaviour
{
    [SerializeField] private EnemyCharacteristics EnemyCharacteristics;
    private void Start()
    {
        EnemyCharacteristics = GetComponentInParent<EnemyCharacteristics>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ÑreatureÑharacteristics>()?.TakeDamage(EnemyCharacteristics.enemyDamage);
        }
    }
}
