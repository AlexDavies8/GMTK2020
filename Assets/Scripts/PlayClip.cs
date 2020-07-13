using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClip : MonoBehaviour
{
    [SerializeField] private float _volume = 1f;

    public void Play(AudioClip clip)
    {
        SoundClipPlayer.PlayClip(clip, _volume);
    }
}
