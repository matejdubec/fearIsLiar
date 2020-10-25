using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class CGameMaster : MonoBehaviour
{

    private SteamVR_LoadLevel loadinator;

    private const string mainMenuName = "MainMenu";

    [Header("Game")]
    [SerializeField]
    private Text scoreText;
    private int score = 0;

    [SerializeField]
    private Transform[] collectibles;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Orbs:\n" + score.ToString() + "/" + collectibles.Length.ToString();
        loadinator = GetComponent<SteamVR_LoadLevel>();
    }

    public void AddScore()
	{
        score++;
        scoreText.text = "Orbs:\n" + score.ToString() + "/" + collectibles.Length.ToString();
    }

    public void ReturnToMenu() {

        loadinator.levelName = mainMenuName;
        loadinator.Trigger();
    }
}
