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
        // ������������ ������ ������� � ����������� �� �����������
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // ������ ������� ������
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // ������ ������� �����
        }
    }
    public void SetDamage(float dmg)
    {
        projectileDamage = dmg;
    }
    private void Start()
    {
        // ���������� ������ ����� ��������� �����
        Destroy(gameObject, projectileLifeTime);
    }

    private void Update()
    {
        // ������� ������ � �������� �����������
        transform.Translate(projectileSpeed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"ColisionDetected" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.TryGetComponent(out �reature�haracteristics characteristics);
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
