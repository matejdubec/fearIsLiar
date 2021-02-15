using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class CLightFlicker : MonoBehaviour
{

    private Light flickeringLight;
    private bool isFlickering = false;
    private float flickerTime;

    // Start is called before the first frame update
    void Start()
    {
        flickeringLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFlickering)
		{
            StartCoroutine("Flicker");
		}
    }

    private IEnumerator Flicker()
	{
        isFlickering = true;
        flickeringLight.enabled = false;
        flickerTime = Random.Range(0.01f, 0.1f);
        yield return new WaitForSeconds(flickerTime);
        flickeringLight.enabled = true;
        flickerTime = Random.Range(1f, 5f);
        yield return new WaitForSeconds(flickerTime);
        isFlickering = false;
    }
}
