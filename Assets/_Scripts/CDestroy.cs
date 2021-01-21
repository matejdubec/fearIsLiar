using UnityEngine;

public class CDestroy : MonoBehaviour
{
	public void destroyObject()
	{
        this.gameObject.SetActive(false);
	}
}