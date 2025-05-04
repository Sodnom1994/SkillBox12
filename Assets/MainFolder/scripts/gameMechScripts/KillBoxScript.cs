using UnityEngine;

public class KillBoxScript : MonoBehaviour
{
    private Player—haracteristics Player—haracteristics;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player—haracteristics = collision.GetComponent<Player—haracteristics>();
            Debug.Log("ColiPLayerKillboxDetect");
            Player—haracteristics.TakeDamage(Player—haracteristics.CurrentHealth);
        }
    }
}
