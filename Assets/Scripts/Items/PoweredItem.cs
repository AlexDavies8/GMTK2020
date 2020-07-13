using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredItem : PlacedItem
{
    [SerializeField] private Transform _attachTransform = null;
    [SerializeField] private AudioClip _powerSound = null;
    [SerializeField] private AudioClip _unpowerSound = null;

    public Transform AttachTransform => _attachTransform;

    public virtual void Power() 
    {
        if (Playing) SoundClipPlayer.PlayClip(_powerSound);
    }
    public virtual void Unpower()
    {
        if (Playing) SoundClipPlayer.PlayClip(_unpowerSound);
    }
}
