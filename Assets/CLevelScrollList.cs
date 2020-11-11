using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLevelScrollList : MonoBehaviour
{
    [SerializeField] private Button buttonTemplate;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddButtons(List<CConfigLevel> levelList)
	{
        foreach(CConfigLevel cLevel in levelList)
		{
            Button newButton = Button.Instantiate(buttonTemplate);
            newButton.transform.SetParent(transform);
            CMainMenuLevelButton levelButton = newButton.GetComponent<CMainMenuLevelButton>();
            levelButton.Setup(cLevel);
		}
	}
}
