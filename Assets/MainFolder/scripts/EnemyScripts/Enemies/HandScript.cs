using UnityEngine;

public class HandScripts : MonoBehaviour, IDamageable
{
    public BossScript BossScript;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float handHealth = 45f;
    [SerializeField] private float nextAttackTime = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (collision.gameObject.TryGetComponent(out ÑreatureÑharacteristics characteristics))
        {
            if (Time.time >= nextAttackTime)
            {
                characteristics.TakeDamage(damage);
                nextAttackTime = Time.time + nextAttackTime;
            }
        }
    }
    public void TakeDamage(float damage)
    {
        handHealth -= damage;

        if (handHealth <= 0f)
        {
            BossScript.OnLimbDestroyed(); // Ñîîáùàåì áîññó, ÷òî ðóêà óíè÷òîæåíà
            Destroy(gameObject); // Óíè÷òîæàåì ðóêó
        }
    }
}
