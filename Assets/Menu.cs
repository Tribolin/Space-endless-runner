using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Highscore;
    public TMPro.TextMeshProUGUI LastScore;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        LastScore.text = "Your Last Score:" + "12";
    }
    public void onButtonPlay()
    {
        SceneManager.LoadScene(0);
    }
}
