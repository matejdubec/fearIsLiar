using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class CMainMenuManager : CLevelManager
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private CLevelScrollList scrollList;

    public override void Init()
	{
        scrollList.Init();
        base.Init();

        if (activeMission)
        {
            activeMission.StartMission();
        }
    }

	public void FilterPhobias(string phobiaString)
	{
		EPhobiaId ePhobiaId = (EPhobiaId)Enum.Parse(typeof(EPhobiaId), phobiaString);

		List<CConfigLevel> filteredLevels = (from level in CGameManager.Instance.MissionController.MissionsDictionary.Values where level.PhobiaId == ePhobiaId select level).ToList();

		scrollList.AddButtons(filteredLevels);
	}

	public void SetVolume(float volume)
	{
		audioMixer.SetFloat("Volume", volume);
	}

    public void QuitApplication()
    {
        Application.Quit();
    }
}
