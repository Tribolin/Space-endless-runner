using UnityEngine;
using UnityEngine.UI;

public class PowerUpCount : MonoBehaviour

{
    public Text Text;
    PlayerMovement player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Text.text = "Slowmo:" + player.SlowMo;
    }
}
