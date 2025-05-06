using UnityEngine;
[RequireComponent (typeof(CircleCollider2D))]
public class Coin : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Debug.Log("Монетка поднята");
        gameObject.SetActive(false);
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
