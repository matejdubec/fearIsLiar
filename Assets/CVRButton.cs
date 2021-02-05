using UnityEngine;

public class CVRButton : MonoBehaviour
{
    private float pressLength = 0.05f;
    private bool pressed;

    private Vector3 origin;
    Rigidbody rb;

    private void Start()
    {
        origin = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float distance = Mathf.Abs(transform.position.y - origin.y);
        if(distance >= pressLength)
        {
            transform.position = new Vector3(transform.position.x, origin.y - pressLength, transform.position.z);
            if(!pressed)
            {
                pressed = true;
            }
        }
        else
        {
            pressed = false;
        }

        if(transform.position.y > origin.y)
        {
            transform.position = new Vector3(transform.position.x, origin.y, transform.position.z);
        }
    }
}
