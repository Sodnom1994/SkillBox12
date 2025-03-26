using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public PointEffector2D explosionEffector;
    public bool explosionBool = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !explosionBool)
        {
            explosionBool = true;
            StartCoroutine(ExplsionEffectOn());
        }
    }
    IEnumerator ExplsionEffectOn()
    {
        explosionEffector.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        explosionEffector.gameObject.SetActive(false);
    }
}
