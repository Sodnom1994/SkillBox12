using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactRange = 5.0f;
    public bool facingRight = true;


    private void Update()
    {
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        Vector2 rayOrigin = (Vector2)transform.position + (direction * 0.25f) + (Vector2.up * 0.25f);

        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, interactRange);
            if (hit.collider != null)
            {
                Debug.Log($"Hit collider: {hit.collider.name}");
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
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        Vector2 rayOrigin = (Vector2)transform.position + (direction * 0.25f) + (Vector2.up * 0.25f);


        if (Input.GetKey(KeyCode.E))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(rayOrigin, direction * interactRange);
        }
    }
}
