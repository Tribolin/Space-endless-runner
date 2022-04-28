using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private List<Highscore> highScores;
    private const string SETTINGS_KEY = "Highscores";

    public List<Highscore> HighScores 
    { 
        get 
        { 
            return highScores ?? Init();
        } 
    }

    private List<Highscore> Init()
    {
        var scorelist = PlayerPrefs.GetString(SETTINGS_KEY);
        Debug.Log($"Scorelist: {scorelist}");

        var list = JsonConvert.DeserializeObject<List<Highscore>>(scorelist);

        if(list == null)
        {
            list = new List<Highscore>();
        }

        Debug.Log($"Scorelist: {list.Count} Elemente");

        highScores = list;
        return highScores;
    }

    public void Save()
    {
        var scorelist = JsonConvert.SerializeObject(highScores);

        Debug.Log(scorelist);

        PlayerPrefs.SetString(SETTINGS_KEY, scorelist);
        PlayerPrefs.Save();
    }

    public void UpdateScores(string name, int score, int max)
    {
        highScores.Add(new Highscore() { Name = name, Score = score });
        highScores = HighScores.OrderByDescending(score => score.Score).Take(max).ToList();
    }
}
