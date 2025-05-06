using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactRange = 5.0f;
    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Debug.Log("Plyer trying interact");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * (transform.localScale.x > 0 ? 1 : -1), interactRange);
            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                interactable?.Interact();
            }
            else
            {
                Debug.Log("Нет предметов для взаимодействия");
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, Vector2.right * interactRange);
        }
    }
}
