﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CSkyscrapersManager : CLevelManager
{
    [SerializeField] private int heightForRespawn = -50;

    public override void Init()
    {
        base.Init();
        if(activeMission)
        {
            activeMission.StartMission();
        }
    }

    private void Update()
    {
        if (CGameManager.Instance.Player.transform.position.y <= heightForRespawn)
        {
            SteamVR_Fade.Start(Color.clear, 0f);
            SteamVR_Fade.Start(Color.black, 1f);
            SpawnPlayer(CGameManager.Instance.Player);
            SteamVR_Fade.Start(Color.black, 0f);
            SteamVR_Fade.Start(Color.clear, 1f);
        }
    }
}
