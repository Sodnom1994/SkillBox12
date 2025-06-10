using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private float openHeight = 2f;
    [SerializeField] private bool isDoorActivated = false;
    private bool isOpening = false;

    private void Update()
    {
        if(isDoorActivated)
        {
            Debug.Log("����� �������� �����������");
            Interact();
            isDoorActivated = false;
        }
    }
    public void Interact()
    {
        if (!isOpening)
        {
            //Debug.Log("����� �������� �����������");
            isOpening = true;
            StartCoroutine(OpenAndDestroy());
        }
    }
    IEnumerator OpenAndDestroy()
    {
        Vector3 targetPosition = transform.position + Vector3.up * openHeight;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }
        transform.position = targetPosition;
        Destroy(gameObject, 5f);
    }
}
