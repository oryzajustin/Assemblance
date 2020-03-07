using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCamera : MonoBehaviour {

    public GameObject FogLeft;
    public GameObject FogRight;
    public ParticleSystem FogParticles;

    void Awake() {
        SetFogSide("left");
    }

    public void SetFogSide(string side)
    {
        if (side == "left")
        {
            FogLeft.SetActive(true);
            FogRight.SetActive(false);
        }
        else
        {
            FogLeft.SetActive(false);
            FogRight.SetActive(true);
        }
        FogParticles.Play();
    }

    public void RemoveFog()
    {
        FogLeft.SetActive(false);
        FogRight.SetActive(false);
        FogParticles.Stop();
    }
}
