using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<GameObject> OnEnemyDeath;
    public static void EnemyDied(GameObject enemy)
    {
        if (enemy == null)
        {
            Debug.LogError("Ошибка: EventBus.EnemyDied вызван с null вместо объекта!");
            return;
        }
        OnEnemyDeath?.Invoke(enemy);
    }

}
