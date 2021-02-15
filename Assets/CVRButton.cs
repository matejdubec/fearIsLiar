using UnityEngine;

public enum EVrButtonColorId
{
    Red,
    Green,
    Blue,
}

public class CVRButton : MonoBehaviour
{
    [SerializeField] private EVrButtonColorId buttonId;
    [SerializeField] private int PressesToComplete = 5;
    private int pressCount = 0;

    private float pressLength = 0.5f;
    private bool pressed;

    private Vector3 origin;
    private Rigidbody rb;
    private CMissionTaskButton buttonTask;
    private bool isActive = false;

    private Material material;
    private float baseEmmitTimer = 0.25f;
    private float currentEmmitTimer;

     public void Init(CMissionTaskButton _buttonTask)
    {
        buttonTask = _buttonTask;
        material = GetComponent<MeshRenderer>().material;
        origin = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        currentEmmitTimer = baseEmmitTimer;
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

            Press();

            /*
            //this.CountPresses();
            

            if (pressCount >= PressesToComplete)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                material.SetFloat("_EmissiveExposureWeight", 1);
                buttonTask.ButtonCompleted(this);               
            }
            */
        }
    }

    private void CountPresses()
    {
        float distance = Mathf.Abs(transform.localPosition.y - origin.y);
        if (distance >= pressLength)
        {
            transform.localPosition = new Vector3(origin.x, origin.y - pressLength, origin.z);
            if (!pressed)
            {
                pressed = true;
                pressCount++;
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

    private void Emit()
	{
        currentEmmitTimer -= Time.deltaTime;

        if (currentEmmitTimer < 0)
        {
            if (material.GetFloat("_EmissiveExposureWeight") == 0)
            {
                material.SetFloat("_EmissiveExposureWeight", 1);
            }
            else
            {
                material.SetFloat("_EmissiveExposureWeight", 0);
            }

            currentEmmitTimer = baseEmmitTimer;
        }
    }

    public void Disable()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        material.SetFloat("_EmissiveExposureWeight", 1);
    }

    public void Press()
    {
        float distance = Mathf.Abs(transform.localPosition.y - origin.y);
        if (distance >= pressLength)
        {
            transform.localPosition = new Vector3(origin.x, origin.y - pressLength, origin.z);
            if (!pressed)
            {
                pressed = true;
                buttonTask.ButtonPressed(this.buttonId);
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
