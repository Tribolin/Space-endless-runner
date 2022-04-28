using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
 
    //public GameObject EmptySpawnZone;
    //public GameObject EmptySpawnPlace;
    public float MoveSpeed = 1f;
    public GameObject Parent;
    public BoxCollider Box;

    CharacterController controller;
    Management manager;
    List<Zone> zones = new List<Zone>();
    int zonecount = 3;
    float zonesize = 0.24f;
    float placeholdersize = (1 - 3 * 0.24f) / 3f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        manager = GameObject.Find("Manager").GetComponent<Management>();
        
        Box.transform.localScale = new Vector3(Box.bounds.size.x ,Box.bounds.size.y,manager.MBCSize);
    }

    void Update()
    {
        Vector3 movement = new Vector3 (0,0,-10f * Time.deltaTime * manager.TSpeed );
        controller.Move(movement );
    }
    
    public void InitZones()
    {
        var size = Parent.GetComponent<MeshRenderer>().bounds.size;
        var width = size.x;
        var height = size.z * zonesize;
        var placHeight = size.z * placeholdersize;
        var positionY = size.z * -0.5f;
        var positionX = size.x * -0.5f;
        var zOffset = size.y / 2;

        for (int i = 0; i < zonecount; i++)
        {
            //Debug.Log("Instantiiere Modul-Zone: " + Parent.name);

            //var zoneObject = Instantiate(EmptySpawnZone, new Vector3(positionX, zOffset, Parent.transform.position.z + positionY), Quaternion.identity, Parent.transform);
            Zone zone = new Zone(positionX, Parent.transform.position.z + positionY, width, height, false,Parent.transform);
            zones.Add(zone);

            ///var placeholderObject = Instantiate(EmptySpawnPlace, new Vector3(positionX, zOffset, Parent.transform.position.z + positionY + height), Quaternion.identity, Parent.transform);
            Zone zoneplaceholder = new Zone( positionX, Parent.transform.position.z + positionY + height, width, placHeight, true,Parent.transform);
            zones.Add(zoneplaceholder);

            positionY += placHeight + height;
        }
    }
}
