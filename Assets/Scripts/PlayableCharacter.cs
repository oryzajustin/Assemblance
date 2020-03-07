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

    private float throwPower = 10f;

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
        if (Input.GetKeyDown(KeyCode.Space)) // yeet the item
        {
            if (itemSlot.childCount != 0)
            {
                HoldableItem heldItem = itemSlot.GetChild(0).GetComponent<HoldableItem>();
                heldItem.transform.parent = null; // detatch object
                //Yeet(otherPlayerGORef.transform.position, 10f, heldItem);
                Yeet(heldItem);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) // drop item
        {
            if(itemSlot.childCount != 0)
            {
                itemSlot.GetChild(0).parent = null;
            }
        }
    }

    private void Yeet(HoldableItem item)
    {
        Rigidbody itemRB = item.transform.GetComponent<Rigidbody>();
        Vector3 direction = otherPlayerGORef.transform.position - this.transform.position;
        itemRB.AddForce(direction * throwPower + Vector3.up * 10f, ForceMode.Impulse);
    }

    //// Throws ball at location with regards to gravity (assuming no obstacles in path) and initialVelocity (how hard to throw the ball)
    //public void Yeet(Vector3 targetLocation, float initialVelocity, HoldableItem item)
    //{
    //    Vector3 direction = (targetLocation - transform.position).normalized;
    //    float distance = Vector3.Distance(targetLocation, transform.position);

    //    float firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
    //    Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, transform.right) * transform.up;
    //    float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
    //    Vector3 velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * initialVelocity;
    //    Debug.Log(velocity);

    //    // throw item
    //    item.transform.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.VelocityChange);
    //}

    //// Helper method to find angle between two points (v1 & v2) with respect to axis n
    //public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    //{
    //    return Mathf.Atan2(
    //        Vector3.Dot(n, Vector3.Cross(v1, v2)),
    //        Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    //}

    //// Helper method to find angle of elevation (ballistic trajectory) required to reach distance with initialVelocity
    //// Does not take wind resistance into consideration.
    //private float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    //{
    //    float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
    //    return angle;
    //}

    public void SetOtherPlayer(GameObject other)
    {
        this.otherPlayerGORef = other;
    }
}
