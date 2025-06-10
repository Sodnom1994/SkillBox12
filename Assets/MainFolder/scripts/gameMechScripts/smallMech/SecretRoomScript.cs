using UnityEngine;

public class SecretRoomScript : MonoBehaviour
{
    [SerializeField] private GameObject secretGate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        secretGate.SetActive(false);
    }
}
