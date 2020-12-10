using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CPlayerData
{
	public bool isTutorialDone;

	public CPlayerData(bool tutorialState)
	{
		this.isTutorialDone = tutorialState;
	}
}