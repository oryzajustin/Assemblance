using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

public class Door : MonoBehaviourPun
{
    public GameObject accompanyingKey;

    [PunRPC] public void DestroyDoor() {
        gameObject.SetActive(false);
        accompanyingKey.transform.parent = null;
        accompanyingKey.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger Enter");
        Key key = other.GetComponent<Key>();
        if (key != null) {
            Debug.Log("Key is not null");
            if (key.AccompanyingDoor == this) {
                Debug.Log("Key matches accompanying door");
                // Match
                //Destroy(gameObject);
                if (PhotonNetwork.IsMasterClient) {
                    photonView.RPC("DestroyDoor", RpcTarget.All);
                }
            }
        }
    }

}
