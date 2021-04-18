using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLevelScrollList : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public void Init()
	{
        if(this.transform.childCount > 0)
        {
            foreach (GameObject child in this.transform)
            {
                Debug.LogError(child.name);
                GameObject.Destroy(child);
            }
        }

        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.transform.SetParent(transform, false);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

        AddButtons();
    }

    private void AddButtons()
    {
        this.ClearButtons();
        foreach (CConfigLevel cLevel in CGameManager.Instance.MissionController.MissionsDictionary.Values)
        {
            if (cLevel.SceneId != ESceneId.MainMenu && cLevel.MissionId != EMissionId.NoMission)
            {
                GameObject button = this.GetPooledObject();
                if (button)
                {
                    CMainMenuLevelButton lButton = button.GetComponent<CMainMenuLevelButton>();
                    lButton.Init(cLevel);
                    button.SetActive(true);
                }
            }
		}
    }

    public void AddButtons(List<CConfigLevel> levels)
    {
        this.ClearButtons();
        foreach (CConfigLevel cLevel in levels)
        {
            GameObject button = this.GetPooledObject();
            if (button)
            {
                CMainMenuLevelButton lButton = button.GetComponent<CMainMenuLevelButton>();
                lButton.Init(cLevel);
                button.SetActive(true);
            }
        }
    } 

    private void ClearButtons()
	{
        for(int i = 0; i < amountToPool; i++)
		{
            pooledObjects[i].SetActive(false);
		}
    }

    private GameObject GetPooledObject()
	{
        for(int i = 0; i < amountToPool; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
                return pooledObjects[i];
			}
		}
        return null;
	}
}
