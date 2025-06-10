using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance;
    private int score = 0;
    [SerializeField] public TextMeshProUGUI scoreTextMeshPro;
    [SerializeField] private TextMeshProUGUI winScoreTextMeshPro;

    private void Awake()
    {
        scoreTextMeshPro = FindTMPByTypeAndName();
        scoreTextMeshPro.text = "Score: " + score;
        winScoreTextMeshPro.text = "������� ����� : " + score;
        Debug.Log("Awake: " + gameObject.name);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateUI();
    }
    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (scoreTextMeshPro != null)
        {
            scoreTextMeshPro.text = "Score: " + score;

        }
        if (winScoreTextMeshPro != null)
        {
            winScoreTextMeshPro.text = "������� ����� : " + score;
        }
    }
    private void Update()
    {
        scoreTextMeshPro = FindTMPByTypeAndName();
        if(SceneManager.sceneCount ==5)
        {
            winScoreTextMeshPro = FindWTMPByTypeAndName();
        }
        if (scoreTextMeshPro != null)
        {
            scoreTextMeshPro.text = "Score: " + score;
            
        }

        if (winScoreTextMeshPro != null && winScoreTextMeshPro.IsActive())
        {
            
            winScoreTextMeshPro.text = "������� ����� : " + score;
            Debug.Log(1);
        }

    }
    private TextMeshProUGUI FindTMPByTypeAndName()
    {
        // ���� ��� ������� � ����������� TextMeshProUGUI �� �����
        TextMeshProUGUI[] allTMPs = Object.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);

        foreach (TextMeshProUGUI tmp in allTMPs)
        {
            // ��������� ��� GameObject'�
            if (tmp.gameObject.name == "ScoreTextMeshPro")
            {
                return tmp; // ����� ������ TMP
            }
        }

        // ���� �� ����� � ���������� null
        Debug.LogWarning("������ � TextMeshProUGUI � ������ 'ScoreText' �� ������.");
        return null;
    }
    private TextMeshProUGUI FindWTMPByTypeAndName()
    {
        // ���� ��� ������� � ����������� TextMeshProUGUI �� �����
        TextMeshProUGUI[] allTMPs = Object.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);

        foreach (TextMeshProUGUI tmp in allTMPs)
        {
            // ��������� ��� GameObject'�
            if (tmp.gameObject.name == "WinScoreTextMeshPro")
            {
                return tmp; // ����� ������ TMP
            }
        }

        // ���� �� ����� � ���������� null
        Debug.LogWarning("������ � TextMeshProUGUI � ������ 'WinScoreTextMeshPro' �� ������.");
        return null;
    }
}
