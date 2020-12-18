﻿using System.Collections;
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
        foreach (CConfigLevel cLevel in CGameManager.Instance.MissionController.MissionsDictionary.Values)
		{
            if(cLevel.SceneId != ELevelId.MainMenu)
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

    public void ClearButtons()
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
