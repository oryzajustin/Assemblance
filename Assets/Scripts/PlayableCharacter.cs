using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayableCharacter : MonoBehaviourPun
{
    public Dimension dimension;
    [SerializeField] Transform itemSlot;
    [SerializeField] GameObject otherPlayerGORef;

    private void Start()
    {
        Player thisPlayer = photonView.Owner;

        thisPlayer.TagObject = this.gameObject;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PlayableCharacter playerScript = player.GetComponent<PlayableCharacter>();
            if(playerScript != this)
            {
                playerScript.otherPlayerGORef = thisPlayer.TagObject as GameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (itemSlot.childCount != 0)
            {
                HoldableItem heldItem = itemSlot.GetChild(0).GetComponent<HoldableItem>();
                Yeet(heldItem);
            }
        }
    }
    private void Yeet(HoldableItem item)
    {
        Vector3 pointA = this.transform.position;
        Vector3 pointB = otherPlayerGORef.transform.position;
    }

    public void SetOtherPlayer(GameObject other)
    {
        this.otherPlayerGORef = other;
    }
}
