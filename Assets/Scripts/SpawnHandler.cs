using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient) 
        {
            PhotonNetwork.Instantiate("PlayerPlaceholder", spawnPoints[0].position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("PlayerSprite", spawnPoints[1].position, Quaternion.identity);
        }
    }
}
