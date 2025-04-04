using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformScript : MonoBehaviour
{
    [SerializeField] private float speed; // Скорость движения платформы
    [SerializeField] private int startingPoint; // Начальная точка
    [SerializeField] private Transform[] points; // Точки перемещения
    private int i; // Индекс текущей точки
    private void Start()
    {

        // Устанавливаем начальную позицию
        if (points.Length > 0)
        {
            transform.position = points[startingPoint].position;
            i = startingPoint;
        }
    }
    private void FixedUpdate()
    {
        // Проверяем, достигла ли платформа текущей точки
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0; // Переход к первой точке
            }
        }
        // Перемещаем платформу к следующей точке через MovePosition
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.fixedDeltaTime);
    }
}
