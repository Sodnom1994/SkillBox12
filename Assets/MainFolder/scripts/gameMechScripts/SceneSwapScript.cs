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
        yield return new WaitForSeconds(3);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // ���������, ���������� �� SceneBox2
        bool isSceneBox2Present = SceneBox2 != null;

        // ���������� ����������� ��������: ����, ���� SceneBox2 �����������, ����� ��������� �����������
        int direction = isSceneBox2Present
            ? (SceneBox1.transform.position.y > SceneBox2.transform.position.y ? -1 : 1)
            : 1; // ������� ������ ����, ���� SceneBox2 �����������
        Debug.Log($"currentSceneIndex = {currentSceneIndex}");
        Debug.Log($"totalScenes = {totalScenes}");

        // ��������� ������ ��������� �����
        int nextSceneIndex = currentSceneIndex + direction;
        Debug.Log($"nextSceneIndex = {nextSceneIndex}");

        // ���������, ��� ������ ��������� � ���������� ���������
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
