using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCamera : MonoBehaviour {

    public GameObject fog;
    public GameObject planeA;
    public GameObject planeB;

    private GameObject localFog;

    void Start() {
        localFog = Instantiate(fog, fog.transform.localPosition, Quaternion.Euler(0, 90f, 0)) as GameObject;
    }

    private void enableFog(GameObject plane) {
        localFog.transform.parent = plane.transform;
        localFog.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void disableFog(GameObject plane) {
        localFog.transform.parent = null;
    }

    public void enableFogForPlaneA() {
        enableFog(planeA);
    }

    public void enableFogForPlaneB() {
        enableFog(planeB);
    }

    public void switchToPlayerA() {
        enableFogForPlaneB();
    }

    public void switchToPlayerB() {
        enableFogForPlaneA();
    }


}
