using System.Collections.Generic;
using UnityEngine;

public class LeafObjectPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private int poolSizePerPrefab = 10; // Сколько каждого типа будет в пуле
    [SerializeField] private bool shouldExpand = true;

    private Queue<GameObject> objectQueue = new Queue<GameObject>();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (var prefab in prefabs)
        {
            for (int i = 0; i < poolSizePerPrefab; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        if (objectQueue.Count > 0)
        {
            GameObject obj = objectQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else if (shouldExpand && prefabs.Count > 0)
        {
            // Если пула не хватает — создаём новый случайный листочек
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
            GameObject obj = Instantiate(randomPrefab, transform);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("Нет свободных объектов в пуле!");
            return null;
        }
    }

    public void ReturnObjectToPool(GameObject leaf)
    {
        leaf.SetActive(false);
        objectQueue.Enqueue(leaf);
    }
}