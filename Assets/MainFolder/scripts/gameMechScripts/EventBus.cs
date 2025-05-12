using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<GameObject> OnEnemyDeath;
    public static void EnemyDied(GameObject enemy)
    {
        if (enemy == null)
        {
            Debug.LogError("������: EventBus.EnemyDied ������ � null ������ �������!");
            return;
        }
        OnEnemyDeath?.Invoke(enemy);
    }

}
