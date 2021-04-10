using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f ,1f)]
    public float volume = 1f;

    public bool loop = false;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.Play();
        source.volume = volume;
        source.loop = loop;
    }

    public void Stop()
    {
        source.Stop();
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }
}


public class CAudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds;

    public void Init()
    {
        foreach(Sound sound in sounds)
        {
            GameObject temp = new GameObject($"Sound_{sound.name}");
            temp.transform.SetParent(this.transform);
            sound.SetSource(temp.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string soundName)
    {
        Sound soundToPlay = sounds.Find(x => x.name == soundName);
        if(soundToPlay != null)
        {
            soundToPlay.Play();
        }
        else
        {
            Debug.LogError($"No sound with name {soundName}");
        }
    }

    public bool IsPlaying(string soundName)
    {
        Sound soundToPlay = sounds.Find(x => x.name == soundName);
        if (soundToPlay != null)
        {
            return soundToPlay.IsPlaying();
        }
        else
        {
            Debug.LogError($"No sound with name {soundName}");
            return false;
        }
    }


    public void StopSound(string soundName)
    {
        Sound soundToStop = sounds.Find(x => x.name == soundName);
        if (soundToStop != null)
        {
            soundToStop.Stop();
        }
        else
        {
            Debug.LogError($"No sound with name {soundName}");
        }
    }
}
