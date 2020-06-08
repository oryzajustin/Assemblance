using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// For characters to detect items beneath them
public class DetectArea : MonoBehaviourPun
{
    [SerializeField] PlayableCharacter selfCharacter;
    private void OnTriggerStay(Collider other)
    {
        selfCharacter.CheckPickUp(other);
    }
}
