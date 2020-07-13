using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsItem : PlacedItem
{
    public Rigidbody2D Rigidbody { get; private set; }

    Vector2Int _editGridPosition;

    public override void Place()
    {
        base.Place();

        _editGridPosition = GridPosition;

        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void EnterPlay()
    {
        base.EnterPlay();

        _editGridPosition = GridPosition;
        Rigidbody.simulated = true;
    }

    public override void EnterEdit()
    {
        base.EnterEdit();

        GridPosition = _editGridPosition;
        Rigidbody.velocity = Vector2.zero;
        Rigidbody.simulated = false;
    }
}
