using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuManager : CLevelManager
{
	[SerializeField] private CLevelScrollList scrollList;

    public override void Init()
	{
        scrollList.Init();
        base.Init();
        activeMission.StartMission();
	}

	public void FilterPhobias(string phobiaString)
	{
		EPhobiaId ePhobiaId = (EPhobiaId)Enum.Parse(typeof(EPhobiaId), phobiaString);

		List<CConfigLevel> filteredLevels = (from level in CGameManager.Instance.MissionController.MissionsDictionary.Values where level.PhobiaId == ePhobiaId select level).ToList();

		scrollList.ClearButtons();
		scrollList.AddButtons(filteredLevels);
	}

	public void SetVolume(float volume)
	{
		audioMixer.SetFloat("Volume", volume);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}
}
