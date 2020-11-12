using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLevelScrollList : MonoBehaviour
{
    [SerializeField] private Button buttonTemplate;
    private List<Button> shownButtons;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddButtons(List<CConfigLevel> levelList, CSceneLoader loader)
	{
        foreach (CConfigLevel cLevel in levelList)
		{
            Button newButton = Button.Instantiate(buttonTemplate);
            newButton.transform.SetParent(transform);
            CMainMenuLevelButton levelButton = newButton.GetComponent<CMainMenuLevelButton>();
            levelButton.Setup(cLevel, loader);
            shownButtons.Add(newButton);
		}
	}

    public void ClearButtons()
	{
        shownButtons.Clear();
    }
}
