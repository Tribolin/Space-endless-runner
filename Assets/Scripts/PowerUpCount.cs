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
    }

    public void DisplayPowerUpCount()
    {
        if (player.SlowMo) { SlowMotion.text = "SlowMotion"; } else { SlowMotion.text = ""; }
        if (player.ExtraLifes > 0) { ExtraLive.text = "Extra Lives:" + player.ExtraLifes.ToString();}
        else { ExtraLive.text = ""; }
        
    }
     public void DisplayPointScore()
    {
        Score.text = manager.Iterations.ToString();

    }
}
