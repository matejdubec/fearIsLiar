using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class CSkyscrapersData : MonoBehaviour
{

    private SteamVR_LoadLevel loadinator;

    [SerializeField]
    private int heightForRespawn = -50;

    [SerializeField]
    private CVRController player;

    private Vector3 playerSpawnPosition;

    private const string mainMenuName = "MainMenu";
    public bool FinishedSuccessfully { get; private set; } = false;

    [Header("Game")]
    [SerializeField]
    private Text scoreText;
    private int score = 0;

    [SerializeField]
    private Transform[] collectibles;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = $"{score}/{collectibles.Length}";
        //loadinator = GetComponent<SteamVR_LoadLevel>();
        player.SetPosition(playerSpawnPosition);
    }

	private void Update()
	{
        if (player.transform.position.y <= heightForRespawn)
        {
            SteamVR_Fade.Start(Color.clear, 0f);
            SteamVR_Fade.Start(Color.black, 1f);
            player.SetPosition(playerSpawnPosition);
            SteamVR_Fade.Start(Color.black, 0f);
            SteamVR_Fade.Start(Color.clear, 1f);
        }
    }

	public void AddScore()
	{
        score++;
        scoreText.text = $"{score}/{collectibles.Length}";
    }

    public void ReturnToMenu() {

        if(score == collectibles.Length)
		{
            FinishedSuccessfully = true;
        }

        loadinator.levelName = mainMenuName;
        loadinator.Trigger();
    }
}
