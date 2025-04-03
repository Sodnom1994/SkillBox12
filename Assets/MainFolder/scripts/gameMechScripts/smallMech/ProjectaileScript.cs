using UnityEngine;

public class ProjectaileScript : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 0.1f;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float projectileDamage;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        // Поворачиваем спрайт снаряда в зависимости от направления
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Спрайт смотрит вправо
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Спрайт смотрит влево
        }
    }
    public void SetDamage(float dmg)
    {
        projectileDamage = dmg;
    }
    private void Start()
    {
        // Уничтожаем снаряд через указанное время
        Destroy(gameObject, projectileLifeTime);
    }

    private void Update()
    {
        // Двигаем снаряд в заданном направлении
        transform.Translate(projectileSpeed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"ColisionDetected" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.TryGetComponent(out СreatureСharacteristics characteristics);
            if (characteristics != null)
            {
                characteristics.TakeDamage(projectileDamage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 0.25f);
        }
    }

}
