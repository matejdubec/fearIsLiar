using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ELevelId 
{ 
	MainMenu = 0, Skyscrapers = 1, ClosingIn = 2
}

public enum EPhobiaId
{
	None = 0, Acrophobia = 1, Achluophobia = 2, Arachnophobia = 3, Claustrophobia = 4,
}

[CreateAssetMenu(menuName = "Configs/Level")]
public class CConfigLevel : ScriptableObject
{
	public ELevelId Id;
	public string Description = "DefaultText.NoText";
	public EPhobiaId PhobiaId;
	public float TimeOfCompletion = 0.0f;
	public Sprite Icon;
}
