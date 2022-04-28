using UnityEngine;
using UnityEngine.UI;

public class PowerUpCount : MonoBehaviour

{
    public Text ExtraLive;
    public Text Score;
    public Text SlowMotion;
    public float Timer = 0;

    PlayerMovement player;
    Management manager;



    void Start()
    {

        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        manager = GameObject.Find("Manager").GetComponent<Management>();
        DisplayPowerUpCount();
    }

    public void DisplayPowerUpCount()
    {
        if (player.SlowMo) { SlowMotion.text = "SlowMotion"; } else { SlowMotion.text = ""; }
        ExtraLive.text = $"Lives: {player.ExtraLifes + 1}";
    }

    public void DisplayPointScore()
    {
        Score.text = manager.Iterations.ToString();
    }
}
