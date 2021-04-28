using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CCameraFade : MonoBehaviour
{
    private float _fadeDuration = 0f;

    [SerializeField] private GameObject blocker;

    public void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("FadeTest"))
        {
            blocker.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FadeTest"))
        {
            blocker.SetActive(false);
        }
    }

    public void TurnOffBlocker()
    {
        blocker.SetActive(false);
    }
}
