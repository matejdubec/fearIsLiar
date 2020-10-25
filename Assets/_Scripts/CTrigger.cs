using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrigger : MonoBehaviour
{
	[SerializeField] private CGameMaster master;

	private void OnTriggerEnter(Collider other)
	{
		master.ReturnToMenu();
	}
}
