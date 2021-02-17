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

public class CColorfulCube : CEmitable
{
    [SerializeField] private EColor color;
    public EColor Color { get { return color; } }
    [SerializeField] private Material dissolve;

    public void Init()
    {
        base.Init(GetComponent<MeshRenderer>().material);
    }

    public void Deactivate()
    {
        material = dissolve;
        this.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        this.Emit();
    }
}
