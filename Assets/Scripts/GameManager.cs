using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Destroy GameManager");
            Destroy(gameObject);
        }
    }

    public int CurrentScore { get; set; }
}
