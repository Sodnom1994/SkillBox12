using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    
    [SerializeField] private IInteractable linkedObject;
    [SerializeField] private Animator animator;


    public void Interact()
    {
        Debug.Log("Рычаг активирован");
        if (linkedObject != null)
        {
            linkedObject?.Interact();
        }
    }
}

