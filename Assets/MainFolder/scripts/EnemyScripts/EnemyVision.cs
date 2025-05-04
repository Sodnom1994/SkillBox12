using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private EnemyCharacteristics EnemyCharacteristics;
    private void Start()
    {
        EnemyCharacteristics = GetComponentInParent<EnemyCharacteristics>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log($"EnemyVision playerDetected");
            EnemyCharacteristics.PlayerDetected();
            Destroy(gameObject, 2f);
        }
    }    
}
