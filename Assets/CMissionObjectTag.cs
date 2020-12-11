using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionObjectTag : MonoBehaviour
{
    [SerializeField] private EMissionId missionId;
    public EMissionId MissionId { get { return missionId; } }
}
