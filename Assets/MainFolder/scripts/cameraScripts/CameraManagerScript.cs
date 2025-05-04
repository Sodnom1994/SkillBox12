using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
public class CameraManagerScript : MonoBehaviour
{
    [Header("Cinemachine cameras")]
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private CinemachineCamera knightCamera;
    [Header("CutScene settings")]
    [SerializeField] private float cameraSwitchDuration = 5.0f;    
    [SerializeField] private PlayerController playerController;
    private Coroutine cameraSwitchCoroutine;
    [SerializeField] private KnightTriggerCutSceneBox knightTriggerCutSceneBox;
    private void Start()
    {
        if (playerCamera == null && knightCamera == null)
        {
            Debug.LogError("Не привязаны камеры к объектам");
        }
    }
    public void SwitchCameraToKnight()
    {
        if (cameraSwitchCoroutine != null)
        {
            StopCoroutine(cameraSwitchCoroutine);
        }
        playerCamera.Priority = 0;
        knightCamera.Priority = 10;
        //Debug.Log("Камера на рыцаря");

        cameraSwitchCoroutine = StartCoroutine(ReturnToPlayerCamera());
    }
    IEnumerator ReturnToPlayerCamera()
    {
        yield return new WaitForSeconds(cameraSwitchDuration);
        playerCamera.Priority = 10;
        knightCamera.Priority = 0;
        //Debug.Log("Камера вернулась");
        knightTriggerCutSceneBox.CinematicBars.SetActive(false);
        playerController.enabled = true;
    }
}
