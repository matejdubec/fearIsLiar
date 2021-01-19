using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CMissionTaskBase : MonoBehaviour
{
    [SerializeField] private string localizationIndentificator = null;
    public string LocalizationIndentificator { get { return localizationIndentificator; } }

    [SerializeField] private float markerOffsetY = 0.0f;
    public float MarkerOffsetY { get { return markerOffsetY; } }

    [SerializeField] protected Animator animator = null;
    [SerializeField] protected string animatorString = "";
    [SerializeField] protected AudioSource audioSource = null;

    protected bool isCurrent = false;

    private CMissionTaskManager missionManager;

    protected virtual void TaskCompleted()
    {
        isCurrent = false;
        missionManager.TaskComplete();
    }

    public virtual void Init(CMissionTaskManager _missionManager)
    {
        this.missionManager = _missionManager;
    }

    public virtual void Activate()
    {
        isCurrent = true;
        this.gameObject.SetActive(true);
    }

    public virtual void Deactivate()
    {
        //this.gameObject.SetActive(false);
    }
}
