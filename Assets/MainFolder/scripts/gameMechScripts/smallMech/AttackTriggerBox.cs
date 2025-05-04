using System.Collections;
using UnityEngine;

public class AttackTriggerBox : MonoBehaviour
{
    private Knight knight;
    private Coroutine AttackCoroutine=null;
    private void Awake()
    {
        knight = GetComponentInParent<Knight>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"AttackCoroutine = {AttackCoroutine}");
        if (collision.CompareTag("Player") && AttackCoroutine == null && knight.IsAlive)
        {
            Debug.Log("AttackingPlayer");
            AttackCoroutine = StartCoroutine(AttackRoutine());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && AttackCoroutine != null && knight.IsAlive)
        {
            StopCoroutine(AttackCoroutine);
            knight.isAttacking = false;
            AttackCoroutine = null;
        }
    }
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            knight.MeleeAttack();
            yield return new WaitForSeconds(knight.knightAttackCooldown);
        }
    }
}
