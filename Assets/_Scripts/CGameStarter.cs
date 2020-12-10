using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CGameManager.CreateInstance();
    }
}
