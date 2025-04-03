using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    public Camera mainCamera;
    public Transform cameraTransform;
    public float parallaxFactor = 0.5f;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
   
    public void Start()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("������� ���������� ParalaxScript");
        }
        lastCameraPosition = cameraTransform.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            textureUnitSizeX = spriteRenderer.sprite.texture.width / spriteRenderer.sprite.pixelsPerUnit * transform.localScale.x;
        }
        else
        {
            Debug.LogError("SpriteRenderer or Sprite is missing on this object!");
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            // ��������� �������� ������
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

            // ��������� ������� ���� � ������ parallaxFactor
            transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0);

            // ��������� ���������� ��������
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offset = (cameraTransform.position.x - transform.position.x) % (textureUnitSizeX / 0.5f);
                transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y, transform.position.z);
            }

            // ��������� ��������� ������� ������
            lastCameraPosition = cameraTransform.position;
        }
        
    }
}
