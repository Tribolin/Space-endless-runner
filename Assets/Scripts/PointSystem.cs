using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointSystem : MonoBehaviour
{
    
    public Text Anzeige;
    public float Timer = 0;
    int anTime = 0;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        anTime = (int)Timer;
        Anzeige.text = anTime.ToString();
    }
}
