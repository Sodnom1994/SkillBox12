using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]   
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private Animator animator;
    private bool isAnimatorSet => animator != null;
    public void Interact()
    {
        if (isAnimatorSet)
        {
            animator.SetTrigger("isOpen");
        }
        Debug.Log("Сундук открылся");
        if (coinPrefab != null && dropPoint != null)
        {
            Instantiate(coinPrefab, dropPoint.position, Quaternion.identity);
        }
        StartCoroutine(CloseChest());

    }
    IEnumerator CloseChest()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("isClose");
    }    
}

