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

    [SerializeField] HoldableItem item;

    public Vector3 direction;

    private float throwPower = 8f;

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
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // yeet the item
            {
                if (itemSlot.childCount != 0)
                {
                    HoldableItem heldItem = itemSlot.GetChild(0).GetComponent<HoldableItem>();
                    heldItem.transform.parent = null; // detatch object
                                                      //Yeet(otherPlayerGORef.transform.position, 10f, heldItem);
                    YeetWrapper(heldItem);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q)) // drop item
            {
                if (itemSlot.childCount != 0)
                {
                    itemSlot.GetChild(0).parent = null;
                }
            }
        }
    }

    public void YeetWrapper(HoldableItem item)
    {
        this.item = item;
        photonView.RPC("Yeet", RpcTarget.All);
    }

    [PunRPC]
    private void Yeet()
    {
        //this.item.transform.parent = null;
        Rigidbody itemRB = item.transform.GetComponent<Rigidbody>();
        itemRB.isKinematic = false;
        itemRB.useGravity = true;
        itemRB.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        //Vector3 direction = otherPlayerGORef.transform.position - this.transform.position;
        itemRB.AddForce(this.direction * throwPower + Vector3.up * 2f, ForceMode.Impulse);

        this.item = null;
    }

    // Detects when you're over an item
    public void CheckPickUp(Collider other)
    {
        if (photonView.IsMine)
        {
            // Picks it up if keypress
            if (Input.GetKeyDown(KeyCode.E) && itemSlot.childCount == 0 && other.GetComponent<HoldableItem>() != null)
            {
                // transfer ownership and pick it up (we do this because by default all scene owned photonviews are owned by the master client)
                other.GetComponent<HoldableItem>().photonView.TransferOwnership(photonView.Owner);
                other.transform.parent = itemSlot.transform;
                other.transform.localPosition = Vector3.zero;
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.Sleep();
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    public void SetOtherPlayer(GameObject other)
    {
        this.otherPlayerGORef = other;
    }
}
