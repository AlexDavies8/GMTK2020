using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClipPlayer : MonoBehaviour
{
    static SoundClipPlayer _instance;

    AudioSource _source;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }
        else _instance = this;

        _source = GetComponent<AudioSource>();
    }

    public static void PlayClip(AudioClip audioClip)
    {
        if (!_instance || !audioClip) return;
        _instance._source.PlayOneShot(audioClip);
    }

    public static void PlayClip(AudioClip audioClip, float volume)
    {
        if (!_instance || !audioClip) return;
        _instance._source.PlayOneShot(audioClip, volume);
    }
}
