using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollideArea : MonoBehaviour
{
    [SerializeField] private EColor color;
    private CColorfulCube cube;
    private CMissionTaskCubes cubeTask;

    public void Init(CMissionTaskCubes _cubeTask)
    {
        cubeTask = _cubeTask;
    }

    private void OnTriggerEnter(Collider other)
    {
       cube = other.GetComponent<CColorfulCube>();

        if (cube && cube.Color == color)
        {
            cube.Deactivate();
            cubeTask.CubeCollected();
        }
    }
}
