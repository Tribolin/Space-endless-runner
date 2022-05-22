using UnityEngine;

public class Zone : MonoBehaviour
{
   
    public bool IsPlaceholder { get; }
    public float StartX { get; }
    public float StartY { get; }
    public float Width { get; }
    public float Height { get; }


    Management manager;



    const float DEFAULT_SPAWN_HEIGHT = 0.5f;

    public Zone( float startX, float startY, float width, float height, bool isplaceholder, Transform parent)
    {
        StartX = startX;
        StartY = startY;
        Width = width;
        Height = height;
        IsPlaceholder = isplaceholder;

        manager = GameObject.Find("Manager").GetComponent<Management>();

        if (!isplaceholder)
        {
            int obstacleBudget = manager.Obstacles.Length;
            Debug.Log("Obstacle count: " + manager.Obstacles.Length);
            while (obstacleBudget > 0)
            {
                var price = SpawnObstacle(obstacleBudget, parent);
                obstacleBudget -= price;
            }

            //Power Up Spawn
            var spawnPowerup = Random.Range(0, 10);
            if (spawnPowerup == 1)
            {
                SpawnPowerUp(parent);  
            }
        }
    }

    private int SpawnObstacle(int budget, Transform Parent)
    {
        Debug.Log("SpawnObstacle");
        //Obstacles Spawn
        var obstacleIndex = Random.Range(0, budget);

        

        float x = Random.Range(StartX, StartX + Width);
        float y =  DEFAULT_SPAWN_HEIGHT;
        float z = Random.Range(StartY, StartY + Height);

        Vector3 spawnPosition = new Vector3(x, y, z);
        var child = Instantiate(manager.Obstacles[obstacleIndex], spawnPosition, Quaternion.identity);
        child.transform.SetParent(Parent);

        return obstacleIndex +1;
    }

    private void SpawnPowerUp(Transform parent)
    {
        var powerUpToSpawn = manager.PowerUps[Random.Range(0, manager.PowerUps.Length)];

        Vector3 spawnPosition = new Vector3(Random.Range(StartX, StartX + Width), 1, Random.Range(StartY, StartY + Height));
        var child = Instantiate(powerUpToSpawn, spawnPosition, Quaternion.identity);
        child.transform.SetParent(parent);
    }
}