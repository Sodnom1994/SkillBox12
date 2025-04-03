using System.Collections;
using UnityEngine;

public class PlayerOneWayPlatformInteraction : CreaturePlatformInteraction
{
    [Header("Adoption Player Setting(назначаются автоматически)")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BoxCollider2D playertBoxCollider2D;

    protected override Transform GroundCheck => playerController.groundCheck;
    protected override BoxCollider2D CreatureBoxCollider2D => playertBoxCollider2D;
    protected override void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playertBoxCollider2D = GetComponent<BoxCollider2D>();
        
    }
    protected override void Update()
    {
        base.Update();        
        PassThroughOnKeyDown();
    }
    #region Присвоение именно той платформы которая под ногами игрока
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (platformCollider != null && platformCollider.gameObject == collision.gameObject)
        {
            currentPlatform = collision.gameObject;
        }
    }
    public override void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("One-way platform"))
        {
            if (platformCollider == null || platformCollider.gameObject != collision.gameObject)
            {
                currentPlatform = null;
            }
        }
    }
    #endregion
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
    private IEnumerator DisableCollision()
    {
        //это усложнение перебирает все типы колайдеров box circle capsule итд
        Collider2D[] platformColliders = currentPlatform.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in platformColliders)
        {
            if (IsInLayerMask(col.gameObject, platformLayer))
            {
                Physics2D.IgnoreCollision(playertBoxCollider2D, col, true);
            }
        }
        yield return new WaitForSeconds(0.4f);
        foreach (Collider2D col in platformColliders)
        {
            if (IsInLayerMask(col.gameObject, platformLayer))
            {
                Physics2D.IgnoreCollision(playertBoxCollider2D, col, false);
            }
        }
    }
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((1 << obj.layer) & layerMask) != 0;
    }
    #endregion       
}
