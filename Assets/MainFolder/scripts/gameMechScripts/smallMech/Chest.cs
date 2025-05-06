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
        if (!awardReceived && IsAnimatorSet)
        {
            //Debug.Log("Ñóíäóê îòêğûëñÿ");
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
    private void PotionDrop()
    {
        if (potionPrefab != null && dropPoint != null)
        {
            Instantiate(potionPrefab, dropPoint.position, Quaternion.identity);
        }
    }
    IEnumerator CloseChest()
    {
        yield return new WaitForSeconds(4f);
        animator.SetTrigger("isClose");
    }
    IEnumerator OpeningChest()
    {
        animator.SetTrigger("isOpen");
        yield return new WaitForSeconds(1.2f);
        CoinRain();
        PotionDrop();
        StartCoroutine(CloseChest());
    }

}

