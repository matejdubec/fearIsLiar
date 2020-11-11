using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

[RequireComponent(typeof(SteamVR_LoadLevel))]
public class CSceneLoader : MonoBehaviour
{
    private SteamVR_LoadLevel loadinator = null;

    private void Start()
    {
        loadinator = GetComponent<SteamVR_LoadLevel>();
    }

    public void LoadScene(string sceneName)
	{
        loadinator.levelName = sceneName;
        loadinator.Trigger();
    }
}