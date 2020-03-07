using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCamera : MonoBehaviour {

    public GameObject FogLeft;
    public GameObject FogRight;
    public ParticleSystem FogParticles;

    public bool FogEnabled = false;
    public float AspectRatio;

    void Awake() {
        SetFogSide("left");
    }

    void Start()
    {
        Camera camera = GetComponentInChildren<Camera>();
        camera.aspect = AspectRatio;
        float windowAspect = (float)Screen.width / (float)Screen.height;

        if (AspectRatio > windowAspect) // Bars on top/bottom
        {
            float diff = 1 - (windowAspect / AspectRatio);
            camera.rect = new Rect(0, (diff / 2.0f), 1f, 1.0f - diff);

        }
        else // AspectRatio < windowAspect, Columns on side
        {
            float diff = 1 - (AspectRatio / windowAspect);
            camera.rect = new Rect((diff / 2.0f), 0, (1.0f - diff), 1f);
        }

        if (!FogEnabled)
        {
            FogLeft.SetActive(false);
            FogRight.SetActive(false);
            FogParticles.Stop();
        }
    }

    public void SetFogSide(string side)
    {
        if (FogEnabled)
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
    }

    public void RemoveFog()
    {
        if (FogEnabled)
        {
            FogLeft.SetActive(false);
            FogRight.SetActive(false);
            FogParticles.Stop();
        }
    }
}
