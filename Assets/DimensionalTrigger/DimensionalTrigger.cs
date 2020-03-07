using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalTrigger : MonoBehaviour
{
    public int Dimension;

    private void OnTriggerEnter(Collider other)
    {
        InterdimensionalObject hitObject = other.GetComponent<InterdimensionalObject>();
        if (hitObject != null)
        {
            hitObject.UpdateCurrentDimension(Dimension);
        }
    }
}
