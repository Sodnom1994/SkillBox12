using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    
    [SerializeField] private IInteractable linkedObject;
    [SerializeField] private Animator animator;


    public void Interact()
    {
        Debug.Log("����� �����������");
        if (linkedObject != null)
        {
            linkedObject?.Interact();
        }
    }
}

