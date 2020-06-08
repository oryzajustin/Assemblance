using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// Handles non-movement behaviour such as item throwing and pick-up
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

    void Update()
    {
        if (photonView.IsMine) // Determines if you're the correct player
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Throw the item
            {
                if (itemSlot.childCount != 0)
                {
                    HoldableItem heldItem = itemSlot.GetChild(0).GetComponent<HoldableItem>();
                    heldItem.transform.parent = null; // Detatch object
                                                      // Yeet(otherPlayerGORef.transform.position, 10f, heldItem);
                    YeetWrapper(heldItem);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q)) // Drop item
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
        Rigidbody itemRB = item.transform.GetComponent<Rigidbody>();
        itemRB.isKinematic = false;
        itemRB.useGravity = true;
        itemRB.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
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
                // Transfer ownership and pick it up (we do this because by default all scene owned photonviews are owned by the master client)
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
}
