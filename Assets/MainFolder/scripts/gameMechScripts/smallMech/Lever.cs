using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject door;
    public void Interact()
    {
        Debug.Log("����� �����������");
        if (door != null)
        {
            var Interactable = door.GetComponent<IInteractable>();
            Interactable?.Interact();
        }
    }
}

