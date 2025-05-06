using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactRange = 5.0f;
    public bool facingRight =true;


    private void Update()
    {
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        if (Input.GetKeyDown(interactKey))
        {
            Debug.Log("Plyer trying interact");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, interactRange);
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
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        if (Input.GetKey(KeyCode.E))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, direction * interactRange);
        }
    }
}
