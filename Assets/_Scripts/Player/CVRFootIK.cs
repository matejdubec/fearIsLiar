using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVRFootIK : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Vector3 footOffset;
    [SerializeField]
    [Range(0,1)]
    private float rightFootPosWeight = 1;
    [SerializeField]
    [Range(0, 1)]
    private float rightFootRotWeight = 1;
    [SerializeField]
    [Range(0, 1)]
    private float leftFootPosWeight = 1;
    [SerializeField]
    [Range(0, 1)]
    private float leftFootRotWeight = 1;

    [SerializeField] private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

	private void OnAnimatorIK(int layerIndex)
	{
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit, layerMask);
		if (hasHit)
		{
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + footOffset);

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
		}
		else
		{
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }

        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        hasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit, layerMask);
        if (hasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + footOffset);

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, footRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}
