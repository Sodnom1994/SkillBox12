using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public ItemData[] lootTable;
    public Transform dropPosition;
    private void OnEnable()
    {
        Debug.Log("LootSpawner: Подписываюсь на событие");
        EventBus.OnEnemyDeath += DropLoot;
    }

    private void OnDisable()
    {
        EventBus.OnEnemyDeath -= DropLoot;
    }
    public void DropLoot(GameObject deadEnemy)
    {
        if (deadEnemy != this.gameObject) return;
        if (lootTable == null || lootTable.Length == 0)
        {
            Debug.LogWarning("Нет выпадающих предметов");
        }
        ItemData selectedLoot = lootTable[Random.Range(0, lootTable.Length)];
        if (selectedLoot != null && selectedLoot.prefab != null)
        {
            int dropCount = Random.Range(selectedLoot.minDropCount, selectedLoot.maxDropCount + 1);
            for (int i = 0; i < dropCount; i++)
            {
                Instantiate(selectedLoot.prefab, dropPosition.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("Нет префаба выпадающих предметов");
        }
    }
}
