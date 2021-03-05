using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionTaskKey : CMissionTaskBase
{
	[SerializeField] private List<CKey> keys;
    private int counter = 0;

    public override void Init(CMissionTaskManager _missionManager)
    {
        base.Init(_missionManager);

        foreach(CKey key in keys)
        {
            key.Init(this);
        }
    }

    public override void Activate()
    {
        base.Activate();

        foreach (CKey key in keys)
        {
            key.Activate();
        }
    }

    public void KeyPickedUp(CKey key)
    {
        if(keys.Find(x => x == key))
        {
            counter++;
            key.Deactivate();
            if (counter >= keys.Count)
            {
                this.TaskCompleted();
            }
        }
    }
}
