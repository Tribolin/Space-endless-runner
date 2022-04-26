using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    public GameObject[] obstacles;
    Queue<GameObject> queue = new Queue<GameObject>();
    public GameObject[] initialState;
    public GameObject[] allModules;

    public float Timeincrease = 1;
    Vector3 spawnpoint;
    Quaternion spawnrotation;
    void Start()
    {
        queue.Enqueue(initialState[0]);
        for (int i = 1; i < initialState.Length; i++)
        {
            queue.Enqueue(initialState[i]);
            initialState[i].GetComponent<Module>().InitZones();
        }
        spawnpoint = allModules[allModules.Length - 1].GetComponent<Transform>().position;
        spawnrotation = allModules[allModules.Length - 1].GetComponent<Transform>().rotation;
    }

    public void SpawnModule()
    {
        var module = allModules[Random.Range(0, allModules.Length)];
        var instance = Instantiate(module, spawnpoint, spawnrotation);

        instance.GetComponent<Module>().InitZones();
       
        queue.Enqueue(instance);
       
    }

    public void DestroyLastModule()
    {
        Destroy(queue.Dequeue(), 1);
    }
}
