using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformScript : MonoBehaviour
{
    [SerializeField] private float speed; // �������� �������� ���������
    [SerializeField] private int startingPoint; // ��������� �����
    [SerializeField] private Transform[] points; // ����� �����������
    private int i; // ������ ������� �����
    private void Start()
    {

        // ������������� ��������� �������
        if (points.Length > 0)
        {
            transform.position = points[startingPoint].position;
            i = startingPoint;
        }
    }
    private void FixedUpdate()
    {
        // ���������, �������� �� ��������� ������� �����
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0; // ������� � ������ �����
            }
        }
        // ���������� ��������� � ��������� ����� ����� MovePosition
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.fixedDeltaTime);
    }
}
