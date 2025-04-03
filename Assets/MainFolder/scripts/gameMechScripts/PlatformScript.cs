using UnityEngine;
using static UnityEditor.UIElements.ToolbarMenu;
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlatformScript : MonoBehaviour
{
    [SerializeField] private float speed; // Исходная скорость мотора
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;
    private int i;

    private void Start()
    {
        transform.position = points[startingPoint].position;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


}
