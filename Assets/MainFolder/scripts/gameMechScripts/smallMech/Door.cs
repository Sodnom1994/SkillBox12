using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("����� �����������");
        transform.position -= Vector3.down * 2f;
        Destroy(gameObject, 5f);
    }
}
