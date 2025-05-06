using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectable
{
    [SerializeField] private float healAmount = 20.0f;
    
    public void Collect()
    {
        Debug.Log("«ÂÎ¸Â ÔÓ‰Ó·‡ÌÓ");
        if (Player—haracteristics.Instance != null)
        {
            Player—haracteristics.Instance.Heal(healAmount);
            gameObject.SetActive(false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (Player—haracteristics.Instance != null)
            {
                Collect();
            }            
            Destroy(gameObject);
        }
    }
}
