using UnityEngine;

public class Zone : MonoBehaviour
{
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
            var toSpawn = Random.Range(0, manager.obstacles.Length);
            var spawnheight = Random.Range(1f, 5f);
            Vector3 spawnPosition = new Vector3(Random.Range(StartX, StartX + width),toSpawn <4 ? 1f:spawnheight, Random.Range(StartY, StartY + height)) ;
            var child = Instantiate(manager.obstacles[toSpawn], spawnPosition, Quaternion.identity);
            child.transform.SetParent(parent);
        }
    } 
    Management manager;
  
    public GameObject GameObject { get;}
    public bool IsPlaceholder { get;}
    public float StartX { get; }
    public float StartY { get; }
    public float Width { get; }

    public float Height { get; }
}
