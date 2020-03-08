using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public ParticleSystem BeaconRed;
    public ParticleSystem BeaconBlue;
    public GameObject Label;

    void Awake()
    {
        BeaconRed.Stop();
        BeaconBlue.Stop();
        Label.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void SetColour(string colour)
    {
        if (colour == "red")
        {
            BeaconRed.Play();
            BeaconBlue.Stop();
        }
        else
        {
            BeaconRed.Stop();
            BeaconBlue.Play();
        }
        Label.SetActive(true);
    }
}
