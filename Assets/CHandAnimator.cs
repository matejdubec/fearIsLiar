using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CHandAnimator : MonoBehaviour
{
	[SerializeField] private SteamVR_Action_Single grabAction = null;

	private Animator animator = null;
	private SteamVR_Behaviour_Pose pose = null;


	// Start is called before the first frame update
	private void Awake()
	{
		animator = GetComponent<Animator>();
		pose = GetComponentInParent<SteamVR_Behaviour_Pose>();

		grabAction[pose.inputSource].onChange += Grab;
	}

	private void OnDestroy()
	{
		grabAction[pose.inputSource].onChange -= Grab;

	}

	private void Grab(SteamVR_Action_Single action, SteamVR_Input_Sources source, float axis, float delta)
	{
		animator.SetFloat("AnimationState", axis);
	}
}
