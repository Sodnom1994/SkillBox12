using UnityEngine;

public class BackButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;


    public void OnPauseButtonClicked()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
}