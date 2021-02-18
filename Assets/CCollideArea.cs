using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollideArea : MonoBehaviour
{
    [SerializeField] private EColor color;
    private CColorfulCube cube;
    private CMissionTaskCubes cubeTask;
    private bool IsActive = false;

    public void Init(CMissionTaskCubes _cubeTask)
    {
        cubeTask = _cubeTask;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(IsActive)
        {
            cube = other.GetComponent<CColorfulCube>();

            if (cube && cube.Color == color)
            {
                cube.Deactivate();
                cubeTask.CubeCollected();
            }
        }
    }
}
