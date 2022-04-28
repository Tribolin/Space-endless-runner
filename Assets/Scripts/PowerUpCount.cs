using UnityEngine;
using UnityEngine.UI;

public class PowerUpCount : MonoBehaviour

{
    public Text ExtraLive;
    public Text Score;
    public Text SlowMotion;
    public float Timer = 0;

    PlayerMovement player;
    GameManager gameManager;

    void Start()
    {

        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        DisplayPowerUpCount();
    }

    public void DisplayPowerUpCount()
    {
        if (player.SlowMo) { SlowMotion.text = "SlowMotion"; } else { SlowMotion.text = ""; }
        ExtraLive.text = $"Lives: {player.ExtraLifes + 1}";
    }

    public void DisplayPointScore()
    {
        Score.text = gameManager.CurrentScore.ToString();
    }
}
