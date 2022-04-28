using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public List<Highscore> HighScores { get; private set; }

    private void Awake()
    {
        HighScores = Load().OrderByDescending(score => score.Score).ToList();
    }

    private List<Highscore> Load()
    {
        var list = new List<Highscore>()
        {
            new Highscore() { Name = "Silas", Score = 80 },
            new Highscore() { Name = "Holger", Score = 35 },
            new Highscore() { Name = "Marcel", Score = 75 }
        };

        return list;
    }

    private void Save(List<Highscore> scores)
    {

    }
}
