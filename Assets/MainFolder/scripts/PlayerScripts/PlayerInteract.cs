using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactRange = 5.0f;
    [SerializeField] private Vector2 interactDirection = Vector2.right;
    private bool shouldDrawGizmo = false;
    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Debug.Log("Plyer trying interact");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, interactDirection, interactRange);
            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                interactable?.Interact();
            }
            else
            {
                Debug.Log("Нет предметов для взаимодействия");
            }
            shouldDrawGizmo = true;
        }
        else
        {
            shouldDrawGizmo = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (shouldDrawGizmo && Camera.main != null)
        {
            Gizmos.color = Color.red;
            Vector2 rayOrigin = transform.position;
            Vector2 rayEnd = rayOrigin + new Vector2(interactDirection.x, interactDirection.y) * interactRange;
            Gizmos.DrawLine(rayOrigin, rayEnd);
            Gizmos.DrawSphere(rayEnd, 0.1f);
        }
    }

}
