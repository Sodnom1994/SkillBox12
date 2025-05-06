using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]   
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform dropPoint;
    public void Interact()
    {
        Debug.Log("Сундук открылся");
        if(coinPrefab != null && dropPoint != null)
        {
            Instantiate(coinPrefab, dropPoint.position, Quaternion.identity);
        }
    }
}

