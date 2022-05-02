using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI HighscoreNames;
    public TMPro.TextMeshProUGUI HighscoreScores;
    public TMPro.TextMeshProUGUI LastScore;
    public TMPro.TMP_InputField NameInputField;
    public GameObject NameInput;
    
    ScoreBoard scoreBoard;
    GameManager gameManager;
    StringBuilder stringBuilderScores = new StringBuilder();
    StringBuilder stringBuilderNames = new StringBuilder();

    const int MAX_SCOREBOARD_ITEMS = 5;

    private void Awake()
    {
        scoreBoard = gameObject.AddComponent<ScoreBoard>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        NameInput.SetActive(false);

        if (gameManager.CurrentScore > 0 && (scoreBoard.HighScores.Count < MAX_SCOREBOARD_ITEMS || gameManager.CurrentScore > scoreBoard.HighScores.Min(s => s.Score)))
        {
            Debug.Log("Updating highscore");
            NameInput.SetActive(true);
        }
        Debug.Log("UI");
        UpdateUI();
    }

    public void UpdateHighscores()
    {
        var name = NameInputField.text;

        scoreBoard.UpdateScores(name, gameManager.CurrentScore, MAX_SCOREBOARD_ITEMS);
        scoreBoard.Save();

        NameInput.SetActive(false);

        UpdateUI();
    }

    private void UpdateUI()
    {
        Cursor.lockState = CursorLockMode.None;
        LastScore.text = $"Your Score: {gameManager.CurrentScore}";

        var scoreText = BuildHighScoreText(scoreBoard);
        
        HighscoreNames.text = scoreText.Item1;
        HighscoreScores.text = scoreText.Item2;
    }

    public void onButtonPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void on_Quit()
    {
        Application.Quit();
    }
    private (string, string) BuildHighScoreText(ScoreBoard board/*, int score*/)
    {
        stringBuilderScores.Clear();
        stringBuilderNames.Clear();

        foreach (var score in board.HighScores)
        {
            stringBuilderScores.AppendLine(score.Score.ToString());
            stringBuilderNames.AppendLine(score.Name);
        }

        if (board.HighScores.Count < MAX_SCOREBOARD_ITEMS)
        {
            for (int i = 0; i < MAX_SCOREBOARD_ITEMS - board.HighScores.Count; i++)
            {
                stringBuilderScores.AppendLine("000");
                stringBuilderNames.AppendLine("-");
            }
        }

        var names = stringBuilderNames.ToString();
        var scores = stringBuilderScores.ToString();

        return (names, scores);
    }

}
