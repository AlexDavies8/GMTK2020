using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSourceItem : PlacedItem
{
    [SerializeField] private Transform _attachTransform = null;
    [SerializeField] private AudioClip _powerSound = null;
    [SerializeField] private AudioClip _unpowerSound = null;
    [SerializeField] private List<WireItem> _wires = new List<WireItem>();

    public Transform AttachTransform => _attachTransform;

    public List<WireItem> Wires { get => _wires; private set => _wires = value; }

    public override void EnterEdit()
    {
        base.EnterEdit();

        Unpower();
    }

    public virtual void Power()
    {
        if (Playing) SoundClipPlayer.PlayClip(_powerSound);
        foreach (var wire in Wires)
        {
            wire.Power();
        }
    }

    public virtual void Unpower()
    {
        if (Playing) SoundClipPlayer.PlayClip(_unpowerSound);
        foreach (var wire in Wires)
        {
            wire.Unpower();
        }
    }

    public void AddWire(WireItem wire)
    {
        Wires.Add(wire);
        wire.OnRemove += () => Wires.Remove(wire);
    }
}
