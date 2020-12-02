using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameMaster : MonoBehaviour
{
    public static CGameMaster Instance { get; private set; }
    [SerializeField] private bool tutorialCompleted = false;
    [SerializeField] private GameObject tutorial;

    [SerializeField] private CBackToMenuCanvasController backToMenuCanvasController;
    public CBackToMenuCanvasController BackToMenuCanvasController { get { return backToMenuCanvasController; } }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        if (tutorialCompleted)
        {
            tutorial.SetActive(false);
        }
        else
        {
            tutorial.SetActive(true);
        }
    }
}
