using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseGun
{
    protected override void PlaySound()
    {
        AudioManager.Instance.Play(AudioNames.PistolSoundName);
    }
}
