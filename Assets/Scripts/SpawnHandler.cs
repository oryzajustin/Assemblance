using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] GameObject threeDCharPrefab;

    private GameObject threeDPlayer;

    private GameObject twoDPlayer;

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            threeDPlayer = PhotonNetwork.Instantiate(threeDCharPrefab.name, spawnPoints[0].position, Quaternion.identity);
            threeDPlayer.GetComponentInChildren<Beacon>().SetColour("red");
            Camera.main.transform.GetComponentInParent<MasterCamera>().SetFogSide("left");
        }
        else
        {
            twoDPlayer = PhotonNetwork.Instantiate("PlayerSprite", spawnPoints[1].position, Quaternion.identity);
            twoDPlayer.GetComponentInChildren<Beacon>().SetColour("blue");
            Camera.main.transform.GetComponentInParent<MasterCamera>().SetFogSide("right");

            // readjust rotation
            twoDPlayer.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}
