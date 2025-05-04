using UnityEngine;

public class KnightTriggerCutSceneBox : MonoBehaviour
{
    private bool triggerKnight = false;
    [SerializeField] private CameraManagerScript cameraManagerScript;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject knight;
    public GameObject CinematicBars;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggerKnight)
        {
            knight.SetActive(true);
            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;
                cameraManagerScript.SwitchCameraToKnight();
                CinematicBars.SetActive(true);
                triggerKnight = true;
                GetComponent<Collider2D>().enabled = false;
            }

        }
    }
}
