using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadCurrentScene()
    {
        int currnetSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currnetSceneIndex);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

}
