using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

}
