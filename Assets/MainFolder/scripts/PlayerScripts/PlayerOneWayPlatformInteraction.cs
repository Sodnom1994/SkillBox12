using System.Collections;
using UnityEngine;

public class PlayerOneWayPlatformInteraction : CreaturePlatformInteraction
{
    [Header("Adoption Player Setting(назначаются автоматически)")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CapsuleCollider2D playerCapsuleCollider2D;

    protected override Transform GroundCheck => playerController.groundCheck;
    protected override CapsuleCollider2D CapsuleCollider2D => playerCapsuleCollider2D;
    protected override void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        
    }
    protected override void Update()
    {
        if (GroundCheck != null)
        {
            platformCollider = Physics2D.OverlapCircle(GroundCheck.transform.position, groundCheckRadius, platformLayer);

        }
        else
        {
            transform.parent.SetParent(null);
            isOnPlatform = false;
        }
        PassThroughOnKeyDown();
    }

    #region Методы для прохождение сквозь платформу при нажатии Down
    private void PassThroughOnKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }
    public override void OnCollisionStay2D(Collision2D collision)
    {
        if (platformCollider != null && platformCollider.gameObject == collision.gameObject)
        {
            isOnPlatform = true;
            collision.gameObject.TryGetComponent<Transform>(out var NewParentTransform);
            currentPlatform = collision.gameObject;
            if (NewParentTransform != null)
            {
                transform.parent = NewParentTransform;
                
                debuggingSettings.currentParentGameObject = collision.gameObject;
                debuggingSettings.currentParentTransform = collision.transform.parent;
            }

        }
    }
    private IEnumerator DisableCollision()
    {
        //это усложнение перебирает все типы колайдеров box circle capsule итд
        Collider2D[] platformColliders = currentPlatform.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in platformColliders)
        {
            if (IsInLayerMask(col.gameObject, platformLayer))
            {
                Physics2D.IgnoreCollision(playerCapsuleCollider2D, col, true);
            }
        }
        yield return new WaitForSeconds(0.4f);
        foreach (Collider2D col in platformColliders)
        {
            if (IsInLayerMask(col.gameObject, platformLayer))
            {
                Physics2D.IgnoreCollision(playerCapsuleCollider2D, col, false);
            }
        }
    }
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((1 << obj.layer) & layerMask) != 0;
    }
    #endregion       
}
