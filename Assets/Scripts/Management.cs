using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    //Array/queues   
    public GameObject[] Obstacles;
    public GameObject[] PowerUps;
    public GameObject[] InitialState;
    public GameObject[] AllModules;

    public float MBCSize;
    public float TSpeed = 1;
    public float TimeIncrease = 1;
    public int Iterations = 0;

    Vector3 spawnpoint;
    Quaternion spawnrotation;

    //TPouwer Up
    float PowerTime = 0;
    float Walkspeed = 0;
    float gravityPU;

    const float SPEED_LIMITER = 10f;
    const float SPEED_LIMITER_INCREMENT = 2f;
    const float POWER_UP_DURATION = 5f;

    Queue<float> queueTime = new Queue<float>();
    Queue<GameObject> queue = new Queue<GameObject>();
    PlayerMovement player;

    void Start()
    {
        //Enqueue Timeincrease two times
        queueTime.Enqueue(TimeIncrease);
        queueTime.Enqueue(TimeIncrease);
        queue.Enqueue(InitialState[0]);

        for (int i = 1; i < InitialState.Length; i++)
        {
            queue.Enqueue(InitialState[i]);
            InitialState[i].GetComponent<Module>().InitZones();
        }

        spawnpoint = AllModules[AllModules.Length - 1].GetComponent<Transform>().position;
        spawnrotation = AllModules[AllModules.Length - 1].GetComponent<Transform>().rotation;

        TimeIncrease = queueTime.Dequeue() + queueTime.Peek();
        queueTime.Enqueue(TimeIncrease);
        TSpeed = Mathf.Sqrt(TimeIncrease);

        //Power-Ups
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        Walkspeed = player.Speed;
        gravityPU = player.Gravity;
    }
    private void Update()
    {
        if (player.SlowMo)
        {
            float divider = 10;

            PowerTime += Time.deltaTime;
            TSpeed = Mathf.Pow(TimeIncrease, 0.2f) / divider;
            player.Speed = Walkspeed / divider;
            player.Gravity = gravityPU / divider;

            if (PowerTime >= POWER_UP_DURATION)
            {
                PowerTime = 0f;
                player.SlowMo = false;
                player.Speed = Walkspeed;
                player.Gravity = gravityPU;
                TSpeed = Mathf.Pow(TimeIncrease, 0.2f);
            }
        }
    }

    public void SpawnModule()
    {
        var module = AllModules[Random.Range(0, AllModules.Length)];
        //var modSpawnpoint = spawnpoint;
        //modSpawnpoint.z += -10f  Mathf.Pow(Timeincrease, 0.3f);

        var instance = Instantiate(module, spawnpoint, spawnrotation);
        instance.GetComponent<Module>().InitZones();

        if (TSpeed > SPEED_LIMITER)
        {
            TimeIncrease += SPEED_LIMITER_INCREMENT;
        }
        else
        {
            TimeIncrease = queueTime.Dequeue() + queueTime.Peek();
            queueTime.Enqueue(TimeIncrease);
        }

        TSpeed = Mathf.Pow(TimeIncrease, 0.2f);
        MBCSize = TSpeed / 9;

        if (TSpeed > 20)
        {
            TSpeed = 20;
        }

        queue.Enqueue(instance);
        Iterations += 1;
    }

    public void DestroyLastModule()
    {
        Destroy(queue.Dequeue(), 1);
    }
}
