using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    Red,
    Green,
    Blue,
    Purple,
}

public class CColorfulCube : MonoBehaviour
{
    [SerializeField] private EColor color;
    public EColor Color { get { return color; } }
    [SerializeField] private Material dissolve;

    public void Deactivate()
    {
        GetComponent<MeshRenderer>().material = dissolve;
        this.GetComponent<Collider>().enabled = false;
    }
}
