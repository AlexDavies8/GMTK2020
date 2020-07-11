using MultiState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : IState
{
    PlayerItem _player;

    public ClimbState(PlayerItem player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.Animator.SetBool("Climbing", true);
    }

    public void OnExit()
    {
        _player.Animator.SetBool("Climbing", false);
    }

    public void Tick()
    {
        Vector2 moveVector = new Vector2(0f, 1f);
        _player.Rigidbody.velocity = moveVector;

        bool rightOpen = !_player.Raycast(new Vector2(_player.Direction * 0.25f, -0.5f + Time.fixedDeltaTime * 1.1f)).collider;
        _player.OnGround = !rightOpen;
        _player.Climbing = !rightOpen;
    }
}
