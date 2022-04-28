using UnityEngine;

public class Zone : MonoBehaviour
{
    public GameObject GameObject { get; }
    public bool IsPlaceholder { get; }
    public float StartX { get; }
    public float StartY { get; }
    public float Width { get; }
    public float Height { get; }


    Management manager;

    const float MAX_SPAWN_HEIGHT = 4f;
    const float MIN_SPAWN_HEIGHT = 0f;
    const float DEFAULT_SPAWN_HEIGHT = 1f;

    public Zone(GameObject gameObject, float startX, float startY, float width, float height, bool isplaceholder, Transform parent)
    {
        GameObject = gameObject;

        StartX = startX;
        StartY = startY;
        Width = width;
        Height = height;
        IsPlaceholder = isplaceholder;

        manager = GameObject.Find("Manager").GetComponent<Management>();

        if (!isplaceholder)
        {
            int obstacleBudget = manager.Obstacles.Length;
            while(obstacleBudget > 0)
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
        //Obstacles Spawn
        var obstacleIndex = Random.Range(0, budget);

        var hasRandomVerticalPosition = obstacleIndex >= 4; // Element 4 and 5 can spawn in the air

        float x = Random.Range(StartX, StartX + Width);
        float y = hasRandomVerticalPosition ? DEFAULT_SPAWN_HEIGHT : Random.Range(MIN_SPAWN_HEIGHT, MAX_SPAWN_HEIGHT);
        float z = Random.Range(StartY, StartY + Height);

        Vector3 spawnPosition = new Vector3(x, y, z);
        var child = Instantiate(manager.Obstacles[obstacleIndex], spawnPosition, Quaternion.identity);
        child.transform.SetParent(Parent);

        return obstacleIndex;
    }

    private void SpawnPowerUp(Transform parent)
    {
        var powerUpToSpawn = manager.PowerUps[Random.Range(0, manager.PowerUps.Length)];

        Vector3 spawnPosition = new Vector3(Random.Range(StartX, StartX + Width), 1, Random.Range(StartY, StartY + Height));
        var child = Instantiate(powerUpToSpawn, spawnPosition, Quaternion.identity);
        child.transform.SetParent(parent);
    }
}