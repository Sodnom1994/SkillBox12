using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectable
{
    [SerializeField] private float healAmount = 20.0f;
    public void Collect()
    {
        Debug.Log("Çåëüå ïîäîáğàíî");
        if (PlayerÑharacteristics.Instance != null)
        {
            PlayerÑharacteristics.Instance.Heal(healAmount);
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
