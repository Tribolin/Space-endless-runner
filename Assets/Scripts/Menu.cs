using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Highscore;
    public TMPro.TextMeshProUGUI LastScore;
    public GameObject NameInput;

    ScoreBoard scoreBoard;
    GameManager gameManager;
    StringBuilder stringBuilder = new StringBuilder();

    const int MAX_SCOREBOARD_ITEMS = 10;

    private void Start()
    {
        NameInput = GameObject.Find("NameInput");   
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreBoard = gameObject.AddComponent<ScoreBoard>();

        NameInput.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        LastScore.text = $"Your Last Score: {gameManager.CurrentScore}";

        Highscore.text = BuildHighScoreText(scoreBoard);
    }
    public void onButtonPlay()
    {
        SceneManager.LoadScene(1);
    }

    private string BuildHighScoreText(ScoreBoard board/*, int score*/)
    {
        stringBuilder.Clear();

        foreach (var score in board.HighScores)
        {
            stringBuilder.AppendLine($"{score.Name}\t:\t\t{score.Score}");
        }

        if(board.HighScores.Count < MAX_SCOREBOARD_ITEMS)
        {
            for(int i = 0; i < MAX_SCOREBOARD_ITEMS - board.HighScores.Count; i++)
            {
                stringBuilder.AppendLine("- \t\t:\t\t0");
            }
        }

        return stringBuilder.ToString();
    }
}
