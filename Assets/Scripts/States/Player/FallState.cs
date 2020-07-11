using MultiState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : IState
{
    PlayerItem _player;

    public FallState(PlayerItem player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.Animator.SetBool("Falling", true);
    }

    public void OnExit()
    {
        _player.Animator.SetBool("Falling", false);
    }

    public void Tick()
    {
        Vector2 moveVector = new Vector2(0f, -1f);
        _player.Rigidbody.velocity = moveVector;

        bool bottomOpen = !_player.Raycast(new Vector2(0f, -0.5f)).collider;
        _player.OnGround = !bottomOpen;
    }
}
