using UnityEngine;

public class ExplosiveTriggerBox : MonoBehaviour
{
    public GameObject explosivePrefab;
    public Transform explosivePosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (explosivePrefab != null && explosivePosition != null)
            {
                Instantiate(explosivePrefab, explosivePosition);
            }
            Destroy(gameObject);
        }
    }
}
