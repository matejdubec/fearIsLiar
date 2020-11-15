using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrigger : MonoBehaviour
{
	[SerializeField] private CSkyscrapersData master;

	private void OnTriggerEnter(Collider other)
	{
		master.ReturnToMenu();
	}
}
