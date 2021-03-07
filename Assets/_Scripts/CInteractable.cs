using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class CInteractable : MonoBehaviour
{
    public CCustomHand ActiveHand { get; private set; } = null;

    public bool IsInteractable { get; set; } = true;

    public void AttachToHand(CCustomHand hand)
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