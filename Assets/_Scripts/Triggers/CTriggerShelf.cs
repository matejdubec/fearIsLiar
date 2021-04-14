using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTriggerShelf : CTriggerBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(isActive)
        {
            base.OnTriggerEnter(other);
            PlayAnimation();

            if(isActive != canRepeat)
            {
                isActive = canRepeat;
            }
        }
    }

    protected override void PlayAnimation()
	{
        base.PlayAnimation();

        animator.Play("Fall");
	}

    protected override void PlaySound()
    {
        base.PlaySound();
        CGameManager.Instance.AudioManager.PlaySound(audioString);
    }
}
