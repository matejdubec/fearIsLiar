using UnityEngine;

public class CVRButton : MonoBehaviour
{
    private float pressLength = 0.5f;
    private bool pressed;

    private Vector3 origin;

    private void Start()
    {
        origin = transform.localPosition;
    }

    private void Update()
    {
        float distance = Mathf.Abs(transform.localPosition.y - origin.y);
        if(distance >= pressLength)
        {
            transform.localPosition = new Vector3(origin.x, origin.y - pressLength, origin.z);
            if(!pressed)
            {
                pressed = true;
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
