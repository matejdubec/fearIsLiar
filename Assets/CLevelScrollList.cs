using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLevelScrollList : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;


	// Start is called before the first frame update
	void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
		{
            tmp = Instantiate(objectToPool);
            tmp.transform.SetParent(transform, false);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
		}
    }

    public void AddButtons(List<CConfigLevel> levelList, CSceneLoader loader)
	{
        foreach (CConfigLevel cLevel in levelList)
		{
            GameObject button = this.GetPooledObject();
			if (button)
			{
                CMainMenuLevelButton lButton = button.GetComponent<CMainMenuLevelButton>();
                lButton.Setup(cLevel, loader);
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
