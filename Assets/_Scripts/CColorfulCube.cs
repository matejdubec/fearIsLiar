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
    private CMissionTaskCubes taskCubes;
    private bool isActive = false;

    public void Init(CMissionTaskCubes _taskCubes)
    {
        taskCubes = _taskCubes;
        base.Init(GetComponent<MeshRenderer>().material);
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        material = dissolve;
        this.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if(isActive)
        {
            this.StartBlinking();
        }
    }
}
