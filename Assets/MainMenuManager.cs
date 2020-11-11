using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField] private CSceneLoader sceneLoader;
	[SerializeField] private List<CConfigLevel> configLevels;
	[SerializeField] private CLevelScrollList scrollList;

	private void Start()
	{
		scrollList.AddButtons(configLevels);
	}
}
