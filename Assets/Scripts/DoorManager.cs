using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject Doors2D;
    public GameObject Doors3D;
    public MasterCamera FogManager;

    private List<GameObject> Doors;

    // Start is called before the first frame update
    void Start()
    {
        Doors = new List<GameObject>();

        Component[] tempDoors = Doors2D.GetComponentsInChildren<Door>();

        foreach (Door d in tempDoors)
        {
            Doors.Add(d.gameObject);
        }

        tempDoors = Doors3D.GetComponentsInChildren<Door>();

        foreach (Door d in tempDoors)
        {
            Doors.Add(d.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool doorExists = false;

        foreach (GameObject door in Doors)
        {
            if (door.activeInHierarchy)
            {
                doorExists = true;
            }
        }

        if (!doorExists)
        {
            FogManager.RemoveFog();
        }
    }
}
