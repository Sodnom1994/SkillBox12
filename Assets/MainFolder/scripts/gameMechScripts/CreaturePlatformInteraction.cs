using UnityEngine;
[System.Serializable]
public class DebuggingSettings
{
    public GameObject currentParentGameObject;
    public Transform currentParentTransform;
}
public abstract class CreaturePlatformInteraction : MonoBehaviour
{
    [SerializeField] protected float groundCheckRadius = 0.2f;
    [Tooltip("����������� ���� �������� ��� ��������������")]
    [SerializeField] protected LayerMask platformLayer;
    [Tooltip("������� �������� ��������� ������� �� ���������")]
    [SerializeField] protected bool isOnPlatform;
    [Header("One-way Platform Settings(����������� �������������)")]
    [Tooltip("������� ��������� GameObject ���������������")]
    [SerializeField] protected GameObject currentPlatform;
    [Tooltip("������� ��������� Collider2D ���������������")]
    [SerializeField] protected Collider2D platformCollider;
    protected abstract Transform GroundCheck { get; }
    protected abstract CapsuleCollider2D CapsuleCollider2D { get; }
    [Header("Debugging(����������� �������������)")]
    [SerializeField] protected DebuggingSettings debuggingSettings;

    protected virtual void Awake()
    {

    }
    protected virtual void Update()
    {
        if (GroundCheck != null)
        {
            platformCollider = Physics2D.OverlapCircle(GroundCheck.transform.position, groundCheckRadius, platformLayer);
        }
        else
        {
            if (transform.CompareTag("Player"))
            {
                transform.parent.SetParent(null);
            }
        }
    }
    #region �������� � �����������
    public virtual void OnCollisionStay2D(Collision2D collision)
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
    #endregion

    //#region ���������� ������� ��� ���������� �� ���������
    //public virtual void AdoptionCreatureOnPlatform()
    //{
    //    if (platformCollider != null)
    //    {
    //        Transform platformRoot = platformCollider.transform;
    //        if (!isOnPlatform)
    //        {
    //            isOnPlatform = true;
    //            currentPlatform = platformRoot.gameObject;
    //            transform.SetParent(platformRoot);                 
    //            debuggingSettings.currentParentGameObject = platformRoot.gameObject;
    //            debuggingSettings.currentParentTransform = platformRoot.transform.parent;
    //        }
    //    }
    //    else
    //    {
    //        if (isOnPlatform)
    //        {
    //            isOnPlatform = false;
    //            currentPlatform = null;
    //            transform.SetParent(null);                
    //            debuggingSettings.currentParentGameObject = null;
    //            debuggingSettings.currentParentTransform = null;
    //        }
    //    }
    //}
    //#endregion
    // ���������� ����� ��� ������ groundCheck
    protected GameObject FindGroundCheck()
    {
        Transform groundCheckTransform = transform.Find("GroundCheck");
        if (groundCheckTransform != null)
        {
            return groundCheckTransform.gameObject;
        }
        Debug.LogWarning("groundCheck �� ������ � ��������");
        return null;
    }
}

