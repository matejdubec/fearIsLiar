using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CEmitable : MonoBehaviour
{
    protected Material material;
    protected float baseEmmitTimer = 0.25f;
    protected float currentEmmitTimer;

    public virtual void Init(Material _material)
    {
        material = _material;
        currentEmmitTimer = baseEmmitTimer;
    }

    protected virtual void Emit()
    {
        currentEmmitTimer -= Time.deltaTime;

        if (currentEmmitTimer < 0)
        {
            if (material.GetFloat("_EmissiveExposureWeight") == 0)
            {
                material.SetFloat("_EmissiveExposureWeight", 1);
            }
            else
            {
                material.SetFloat("_EmissiveExposureWeight", 0);
            }

            currentEmmitTimer = baseEmmitTimer;
        }
    }
}
