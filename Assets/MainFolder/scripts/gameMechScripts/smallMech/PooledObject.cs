using System;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private Action<GameObject> onReturnToPool;
    private float lifetime;

    public void Initialize(Action<GameObject> returnAction, float lifeTime)
    {
        this.onReturnToPool = returnAction;
        this.lifetime = lifeTime;

        // ������� ������ ��������, ���� ��� ����
        CancelInvoke();
        Invoke("Deactivate", lifeTime);
    }

    private void Deactivate()
    {
        onReturnToPool?.Invoke(gameObject);
    }

    private void OnBecameInvisible()
    {
        // �������������� ��������, ���� ������� �� �����
        onReturnToPool?.Invoke(gameObject);
    }
}
