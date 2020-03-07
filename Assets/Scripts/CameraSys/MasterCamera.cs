using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCamera : MonoBehaviour {

    public GameObject FogLeft;
    public GameObject FogRight;
    public GameObject FogParticles;

    void Awake() {
        SetFog("left");
    }

    public void SetFog(string side)
    {
        if (side == "left")
        {

        }
        else
        {

        }
    }
}
