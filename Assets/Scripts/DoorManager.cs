using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Logic for unlocking doors
public class DoorManager : MonoBehaviour
{
    public GameObject Doors2D;
    public GameObject Doors3D;
    public MasterCamera FogManager;

    private List<GameObject> Doors;

    void Start()
    {
        // Detects all the doors and saves them in list
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

    void Update()
    {
        // Checks if all the doors have unlocked. If so, end the level.

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
