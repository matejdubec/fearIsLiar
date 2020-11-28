using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://answers.unity.com/questions/1490192/how-can-i-highlight-the-vive-controller-buttons.html
public class CTutorial : MonoBehaviour
{

    [SerializeField] private GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void WaypointReached()
	{

	}
}
