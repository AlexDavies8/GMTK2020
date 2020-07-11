using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSourceItem : PlacedItem
{
    List<Wire> _wires = new List<Wire>();

    public void Power()
    {
        foreach (var wire in _wires)
        {
            wire.Destination.Power();
        }
    }

    public void Unpower()
    {
        foreach (var wire in _wires)
        {
            wire.Destination.Unpower();
        }
    }

    public override void Remove()
    {
        Unpower();

        foreach (var wire in _wires)
        {
            wire.Remove();
        }

        base.Remove();
    }
}
