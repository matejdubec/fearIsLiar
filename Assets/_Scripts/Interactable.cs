using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    public CustomHand ActiveHand { get; private set; } = null;

    public bool IsInteractable { get; set; } = true;

    public void AttachToHand(CustomHand hand)
    {
        if(IsInteractable)
        {
            ActiveHand = hand;
        }
    }

    public void DeattachFromHand()
    {
        ActiveHand = null;
    }
}