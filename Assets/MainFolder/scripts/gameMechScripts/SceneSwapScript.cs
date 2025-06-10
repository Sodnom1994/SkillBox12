using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapScript : MonoBehaviour
{
    public Transform SceneBox1;
    public Transform SceneBox2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SceneSwapCoru());
        }
    }

    IEnumerator SceneSwapCoru()
    {
        yield return new WaitForSeconds(3f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int direction = 1; // по умолчанию движемся вперёд

        if (SceneBox2 != null && SceneBox1 != null)
        {
            direction = SceneBox1.position.y > SceneBox2.position.y ? -1 : 1;
        }

        int nextSceneIndex = currentSceneIndex + direction;

        int totalScenes = SceneManager.sceneCountInBuildSettings;

        //Debug.Log($"currentSceneIndex = {currentSceneIndex}");
        //Debug.Log($"nextSceneIndex = {nextSceneIndex}");

        if (nextSceneIndex >= 0 && nextSceneIndex < totalScenes)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load in this direction!");
        }
    }

}
