using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggerZombie : CTriggerBase
{
    [SerializeField] private CEnemyAgentBase agent;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        agent.StandUp();
    }

    protected override void PlaySound()
    {
        base.PlaySound();   
    }
}
