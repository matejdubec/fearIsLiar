using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] private CLevelScrollList scrollList;
	[SerializeField] private AudioMixer audioMixer;

	[SerializeField] private List<CMissionManager> missions;

	[SerializeField] private Transform spawnPosition;

	public void Init()
	{
		scrollList.Init();
		scrollList.AddButtons();

		if (EMissionId.Menu_Tutorial == CGameManager.Instance.MissionController.ActiveMission.MissionId)
		{
			var activeMission = missions.Find(x => x.MissionId == EMissionId.Menu_Tutorial);
			activeMission.Init();
			activeMission.StartMission();
		}
	}

	public void FilterPhobias(string phobiaString)
	{
		EPhobiaId ePhobiaId = (EPhobiaId)Enum.Parse(typeof(EPhobiaId), phobiaString);

		List<CConfigLevel> filteredLevels = (from level in CGameManager.Instance.MissionController.MissionsList where level.PhobiaId == ePhobiaId select level).ToList();

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
