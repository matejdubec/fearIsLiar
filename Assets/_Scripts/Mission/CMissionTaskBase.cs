using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CMissionTaskBase : MonoBehaviour
{
    [SerializeField] private string localizationIndentificator = null;
    public string LocalizationIndentificator { get { return localizationIndentificator; } }

    [SerializeField] private float markerOffsetY = 0.0f;
    public float MarkerOffsetY { get { return markerOffsetY; } }
    [SerializeField] private bool showMarker = true;
    public bool ShowMarker { get { return showMarker; } }

    [SerializeField] private Animator animator = null;
    [SerializeField] private string animatorString = "";
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private List<GameObject> showObjectsOnActive;
    [SerializeField] private List<GameObject> HideObjectsOnActive;

    protected bool isCurrent = false;

    private CMissionTaskManager missionManager;

    protected virtual void TaskCompleted()
    {
        isCurrent = false;

        if (animator & animatorString != "")
        {
            animator.Play(animatorString);
        }

        if (audioSource)
        {
            audioSource.Play();
        }

        missionManager.TaskComplete();
    }

    public virtual void Init(CMissionTaskManager _missionManager)
    {
        this.missionManager = _missionManager;

        foreach (GameObject go in showObjectsOnActive)
        {
            go.SetActive(false);
        }
    }

    public virtual void Activate()
    {
        isCurrent = true;
        this.gameObject.SetActive(true);
        foreach(GameObject go in showObjectsOnActive)
        {
            go.SetActive(true);
        }
    }

    public virtual void Deactivate()
    {
        foreach (GameObject go in showObjectsOnActive)
        {
            go.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }
}
