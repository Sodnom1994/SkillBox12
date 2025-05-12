using UnityEngine;

public class RandomLeafSpawner : MonoBehaviour
{
    [SerializeField] private LeafObjectPool leafObjectPool;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    private void SpawnObject()
    {
        Vector2 randomPoint = GetRandomPointInBox(spawnArea);
        GameObject obj = leafObjectPool.GetPooledObject();

        if (obj != null)
        {
            obj.transform.position = randomPoint;
            obj.SetActive(true);

            PooledObject pooledObj = obj.GetComponent<PooledObject>();
            if (pooledObj != null)
            {
                pooledObj.Initialize(ReturnToPool, lifeTime);
            }
        }
    }

    private Vector2 GetRandomPointInBox(BoxCollider2D box)
    {
        Bounds bounds = box.bounds;
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }

    private void ReturnToPool(GameObject obj)
    {
        leafObjectPool.ReturnObjectToPool(obj);
    }
}