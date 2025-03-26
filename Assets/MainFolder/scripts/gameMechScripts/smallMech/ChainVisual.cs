using UnityEngine;

public class ChainVisual : MonoBehaviour
{
    public SpriteRenderer chainSprite;
    public Transform firstObject;
    public Transform secondObject;
    public float spriteHeight = 1f;
    private void Start()
    {
        if (firstObject == null || secondObject == null || chainSprite == null)
        {
            Debug.LogWarning("Объекты не присвоены ChainVisual");
        }
    }
    private void Update()
    {
        Vector2 direction2D = firstObject.position - secondObject.position;
        float distance = direction2D.magnitude;
        transform.position = Vector3.Lerp(firstObject.position, secondObject.position, 0.1f);
        Vector2 scale = chainSprite.transform.localScale;
        scale.y = distance / spriteHeight;
        chainSprite.transform.localScale = scale;
        float angle = -Mathf.Atan2(direction2D.x, direction2D.y) * Mathf.Rad2Deg;
        chainSprite.transform.rotation = Quaternion.Euler(0f , 0f, angle);
    }
}
