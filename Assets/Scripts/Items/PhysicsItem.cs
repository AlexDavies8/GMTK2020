using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsItem : PlacedItem
{
    Vector2Int _editGridPosition;
    Rigidbody2D _rigidbody;

    public override void Place()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void EnterPlay()
    {
        _editGridPosition = GridPosition;
        _rigidbody.simulated = true;
    }

    public override void EnterEdit()
    {
        GridPosition = _editGridPosition;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.simulated = false;
    }
}
