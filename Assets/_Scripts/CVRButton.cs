using UnityEngine;

public class CVRButton : CEmitable
{
    [SerializeField] private EColor buttonColor;
    public EColor ButtonColor { get { return buttonColor; } }

    private float pressLength = 0.5f;
    private bool pressed;

    private Vector3 origin;
    private Rigidbody rb;
    private CMissionTaskButton buttonTask;
    private bool isActive = false;

     public void Init(CMissionTaskButton _buttonTask)
    {
        buttonTask = _buttonTask;
        base.Init(GetComponent<MeshRenderer>().material);
        origin = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            if (rb.constraints == RigidbodyConstraints.FreezeAll)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }

            this.Emit();
            this.IsPressed();
        }
    }

    public void Disable()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        material.SetFloat("_EmissiveExposureWeight", 1);
    }

    public void IsPressed()
    {
        float distance = Mathf.Abs(transform.localPosition.y - origin.y);
        if (distance >= pressLength)
        {
            transform.localPosition = new Vector3(origin.x, origin.y - pressLength, origin.z);
            if (!pressed)
            {
                pressed = true;
                buttonTask.ButtonPressed(this);
            }
        }
        else
        {
            pressed = false;
            transform.localPosition = new Vector3(origin.x, transform.localPosition.y, origin.z);
        }

        if (transform.localPosition.y > origin.y)
        {
            transform.localPosition = new Vector3(origin.x, origin.y, origin.z);
        }
    }
}
