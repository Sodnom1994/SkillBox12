using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool shouldExpand = true;

    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    private void Awake()
    {

    }
    public void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }
    public GameObject GetPoodedObject()
    {
        if (objectQueue.Count > 0)
        {
            GameObject obj = objectQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else if (shouldExpand)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("Нет свободных объектов в пуле");
            return null;
        }
    }
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectQueue.Enqueue(obj);
    }
}
