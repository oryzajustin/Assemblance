using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Handles player assignment during scene start
public class SpawnHandler : MonoBehaviour
{
    [SerializeField] 
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject threeDCharPrefab;

    private GameObject threeDPlayer;
    private GameObject twoDPlayer;

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient) // Set as 3D player
        {
            threeDPlayer = PhotonNetwork.Instantiate(threeDCharPrefab.name, spawnPoints[0].position, Quaternion.identity);
            threeDPlayer.GetComponentInChildren<Beacon>().SetColour("red");
            Camera.main.transform.GetComponentInParent<MasterCamera>().SetFogSide("left");
        }
        else // Set as 2D player
        {
            twoDPlayer = PhotonNetwork.Instantiate("PlayerSprite", spawnPoints[1].position, Quaternion.identity);
            twoDPlayer.GetComponentInChildren<Beacon>().SetColour("blue");
            Camera.main.transform.GetComponentInParent<MasterCamera>().SetFogSide("right");

            // Re-adjust rotation
            twoDPlayer.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}
