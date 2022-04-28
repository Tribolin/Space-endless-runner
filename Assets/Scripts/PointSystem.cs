using UnityEngine;
using UnityEngine.UI;
public class PointSystem : MonoBehaviour
{
    public Text Display;
    public float Timer = 0;
    
    Management manager;
    
    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Management>();
    }

    void Update()
    {
        Display.text = manager.Iterations.ToString();
    }
}
