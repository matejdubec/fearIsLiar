using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggerWaypoint : CTriggerBase
{

   [SerializeField] private float repeatSoundIn = 20f;
    private float repeatSoundCounter = 0f;
    private bool readyToPlay = true;


    private void Start()
    {
        repeatSoundCounter = repeatSoundIn;
    }

    private void Update()
    {       
        if(canRepeat && !readyToPlay)
        {
            repeatSoundCounter -= Time.deltaTime;

            if(repeatSoundCounter <= 0f)
            {
                repeatSoundCounter = repeatSoundIn;
                readyToPlay = true;
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (readyToPlay)
        {
            this.PlaySound();
            readyToPlay = false;
        }
    }

    protected override void PlaySound()
    {
        base.PlaySound();

        CGameManager.Instance.AudioManager.PlaySound(audioString);
    }
}
