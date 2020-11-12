using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] private List<CConfigLevel> configLevels;
	[SerializeField] private CLevelScrollList scrollList;
	[SerializeField] private CSceneLoader loader;

	private void Start()
	{
		scrollList.AddButtons(configLevels, loader);
	}

	public void FilterPhobias(string phobiaString)
	{
		EPhobiaId ePhobiaId = (EPhobiaId)Enum.Parse(typeof(EPhobiaId), phobiaString);

		List<CConfigLevel> filteredLevels = (from level in configLevels where level.PhobiaId == ePhobiaId select level).ToList();

		scrollList.ClearButtons();
		scrollList.AddButtons(filteredLevels, loader);
	}
}
