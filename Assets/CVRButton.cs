using UnityEngine;

public class CVRButton : MonoBehaviour
{
    [SerializeField] private int PressesToComplete = 5;
    private int pressCount = 0;

    private float pressLength = 0.5f;
    private bool pressed;

    private Vector3 origin;
    private Rigidbody rb;
    private CMissionTaskButton buttonTask;

    private bool isActive = false;

    public void Init(CMissionTaskButton _buttonTask)
    {
        buttonTask = _buttonTask;
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

            this.CountPresses();

            if (pressCount >= PressesToComplete)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                buttonTask.ButtonCompleted(this);
            }
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
}
