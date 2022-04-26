using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
 
    public GameObject emptySpawnZone;
    public GameObject emptySpawnPlac;
    public float MoveSpeed = 0.1f;
    public GameObject Parent;
    

    Management manager;

    CharacterController controller;
    List<Zone> zones = new List<Zone>();
    int zonecount = 3;
    float zonesize = 0.24f;
    float placeholdersize = (1 - 3 * 0.24f) / 3f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //InitModules();
    }

    void Update()
    {
        
        Vector3 movement = transform.forward * -1 * MoveSpeed ;
        controller.Move(movement);
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
            Debug.Log("Instantiiere Modul-Zone: " + Parent.name);

            var zoneObject = Instantiate(emptySpawnZone, new Vector3(positionX, zOffset, Parent.transform.position.z + positionY), Quaternion.identity, Parent.transform);
            Zone zone = new Zone(zoneObject, positionX, Parent.transform.position.z + positionY, width, height, false,Parent.transform);
            zones.Add(zone);

            var placeholderObject = Instantiate(emptySpawnPlac, new Vector3(positionX, zOffset, Parent.transform.position.z + positionY + height), Quaternion.identity, Parent.transform);
            Zone zoneplaceholder = new Zone(placeholderObject, positionX, Parent.transform.position.z + positionY + height, width, placHeight, true,Parent.transform);
            zones.Add(zoneplaceholder);

            positionY += placHeight + height;
        }
    }

}
