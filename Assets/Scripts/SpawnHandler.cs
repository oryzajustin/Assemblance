using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    private GameObject threeDPlayer;

    private GameObject twoDPlayer;

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient) 
        {
            threeDPlayer = PhotonNetwork.Instantiate("PlayerPlaceholder", spawnPoints[0].position, Quaternion.identity);
        }
        else
        {
            twoDPlayer = PhotonNetwork.Instantiate("PlayerSprite", spawnPoints[1].position, Quaternion.identity);
            // readjust rotation
            twoDPlayer.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        PlayableCharacter player1 = null;
        PlayableCharacter player2 = null;

        foreach(GameObject player in players)
        {
            PlayableCharacter playerScript = player.GetComponent<PlayableCharacter>();
            if(playerScript.dimension == Dimension.three)
            {
                player1 = playerScript;
            }
            else
            {
                player2 = playerScript;
            }
        }
        if(player1 != null && player2 != null)
        {
            player1.SetOtherPlayer(player2.gameObject);
            player2.SetOtherPlayer(player1.gameObject);
        }

    }
}
