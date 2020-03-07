using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalTrigger : MonoBehaviour
{
    [SerializeField] Dimension dimension;

    private void OnTriggerEnter(Collider other)
    {
        InterdimensionalObject hitObject = other.GetComponent<InterdimensionalObject>();
        if (hitObject != null)
        {
            hitObject.UpdateCurrentDimension(dimension);
        }
    }
}
