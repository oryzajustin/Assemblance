using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    public Dimension dimension;
    [SerializeField] Transform itemSlot;
    [SerializeField] GameObject otherPlayerGORef;

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
