using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ESceneId 
{
    None = 0, MainMenu = 1, Skyscrapers = 2, ClosingIn = 3, NoLevel = 4, Hospital = 5,
}

public enum EPhobiaId
{
	None = 0, Acrophobia = 1, Achluophobia = 2, Arachnophobia = 3, Claustrophobia = 4, NoPhobia = 5, Nyctophobia = 6,
}

[CreateAssetMenu(menuName = "Configs/Level")]
public class CConfigLevel : ScriptableObject
{
	public EMissionId MissionId;
	public ESceneId SceneId;
	public string Description = "DefaultText.NoText";
	public EPhobiaId PhobiaId;
	public float TimeOfCompletion = 0.0f;
	public Sprite Icon;
}
