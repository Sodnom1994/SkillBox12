using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private int numberOfÑoins = 5;
    [SerializeField] private bool awardReceived = false;
    private bool IsAnimatorSet => animator != null;
    public void Interact()
    {
        if(!awardReceived)
        {
            if (IsAnimatorSet)
            {
                animator.SetTrigger("isOpen");
            }
            Debug.Log("Ñóíäóê îòêğûëñÿ");
            StartCoroutine(OpeningChest());
            
            awardReceived = true;
        }

    }
    private void CoinRain()
    {
        if (coinPrefab != null && dropPoint != null)
        {
            for (int i = 0; i < numberOfÑoins; i++)
            {
                Instantiate(coinPrefab, dropPoint.position, Quaternion.identity);
            }
        }
    }
    IEnumerator CloseChest()
    {
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("isClose");
    }
    IEnumerator OpeningChest()
    {
        yield return new WaitForSeconds(0.4f);
        CoinRain();
        StartCoroutine(CloseChest());
    }

}

