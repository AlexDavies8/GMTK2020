using MultiState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    PlayerItem _player;

    public WalkState(PlayerItem player)
    {
        _player = player;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        Vector2 moveVector = new Vector2(_player.Direction * _player.MovementSpeed, 0f);
        _player.Rigidbody.velocity = moveVector;

        bool frontOpen = !_player.Raycast(new Vector2(_player.Direction * 0.4f, 0f)).collider;
        bool topFrontOpen = !_player.Raycast(new Vector2(_player.Direction * 0.4f, 1f)).collider;

        if (!frontOpen && !topFrontOpen)
        {
            _player.Direction = -_player.Direction;
        }
        else if (!frontOpen)
        {
            _player.Climbing = true;
        }

        bool bottomOpen = !_player.Raycast(new Vector2(_player.Direction * -0.4f, -0.75f)).collider && !_player.Raycast(new Vector2(_player.Direction * 0.4f, -0.75f)).collider;
        _player.OnGround = !bottomOpen;
    }
}
