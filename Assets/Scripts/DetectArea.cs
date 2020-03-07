using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    [SerializeField] Transform itemSlot;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && itemSlot.childCount == 0 && other.GetComponent<HoldableItem>() != null)
        {
            other.transform.parent = itemSlot.transform;
            other.transform.localPosition = Vector3.zero;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.Sleep();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}
