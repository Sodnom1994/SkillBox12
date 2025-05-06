using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapScript : MonoBehaviour
{
    public Transform SceneBox1;
    public Transform SceneBox2;
    private void Start()
    {     
        //if (SceneBox1 == null)
        //{

        //    //Debug.LogWarning("Не удалось найти один или оба объекта SceneBox!");
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SceneSwapCoru());
        }
    }
    IEnumerator SceneSwapCoru()
    {
        yield return new WaitForSeconds(3);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // Проверяем, существует ли SceneBox2
        bool isSceneBox2Present = SceneBox2 != null;

        // Определяем направление перехода: вниз, если SceneBox2 отсутствует, иначе вычисляем направление
        int direction = isSceneBox2Present
            ? (SceneBox1.transform.position.y > SceneBox2.transform.position.y ? -1 : 1)
            : 1; // Переход всегда вниз, если SceneBox2 отсутствует
        Debug.Log($"currentSceneIndex = {currentSceneIndex}");
        Debug.Log($"totalScenes = {totalScenes}");

        // Вычисляем индекс следующей сцены
        int nextSceneIndex = currentSceneIndex + direction;
        Debug.Log($"nextSceneIndex = {nextSceneIndex}");

        // Проверяем, что индекс находится в допустимом диапазоне
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
