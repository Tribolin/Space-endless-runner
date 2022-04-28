using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(-45, 45, 45);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
