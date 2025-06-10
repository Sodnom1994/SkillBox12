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
        winScoreTextMeshPro.text = "Набрано очков : " + score;
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
            winScoreTextMeshPro.text = "Набрано очков : " + score;
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
            
            winScoreTextMeshPro.text = "Набрано очков : " + score;
            Debug.Log(1);
        }

    }
    private TextMeshProUGUI FindTMPByTypeAndName()
    {
        // Ищем все объекты с компонентом TextMeshProUGUI на сцене
        TextMeshProUGUI[] allTMPs = Object.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);

        foreach (TextMeshProUGUI tmp in allTMPs)
        {
            // Проверяем имя GameObject'а
            if (tmp.gameObject.name == "ScoreTextMeshPro")
            {
                return tmp; // Нашли нужный TMP
            }
        }

        // Если не нашли — возвращаем null
        Debug.LogWarning("Объект с TextMeshProUGUI и именем 'ScoreText' не найден.");
        return null;
    }
    private TextMeshProUGUI FindWTMPByTypeAndName()
    {
        // Ищем все объекты с компонентом TextMeshProUGUI на сцене
        TextMeshProUGUI[] allTMPs = Object.FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);

        foreach (TextMeshProUGUI tmp in allTMPs)
        {
            // Проверяем имя GameObject'а
            if (tmp.gameObject.name == "WinScoreTextMeshPro")
            {
                return tmp; // Нашли нужный TMP
            }
        }

        // Если не нашли — возвращаем null
        Debug.LogWarning("Объект с TextMeshProUGUI и именем 'WinScoreTextMeshPro' не найден.");
        return null;
    }
}
