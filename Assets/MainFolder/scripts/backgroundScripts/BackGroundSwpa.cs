using UnityEngine;

public class BackGroundSwpa : MonoBehaviour
{
    public GameObject MountiansBackground;
    public GameObject WaterBackground;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MountiansBackground.SetActive(false);
            WaterBackground.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MountiansBackground.SetActive(true);
            WaterBackground.SetActive(false);
        }
    }
}
