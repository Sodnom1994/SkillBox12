using System.Collections;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private ParticleSystem explosionParticle;
    public float explosionForce;
    public float explosionRadius;
    public LayerMask layerToImpact;
    private void Start()
    {
        explosionParticle = GetComponent<ParticleSystem>();
        StartCoroutine(ExplosionEffectOn());
    }
    IEnumerator ExplosionEffectOn()
    {
        ApplyExplosionForce();
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
    public void ApplyExplosionForce()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius,layerToImpact);
        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out Rigidbody2D rb))
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                rb.AddForce(direction*explosionForce,ForceMode2D.Impulse);
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Визуализируем радиус взрыва в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
}
