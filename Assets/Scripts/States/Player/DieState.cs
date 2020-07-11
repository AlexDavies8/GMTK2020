using MultiState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState
{
    PlayerItem _player;

    public DieState(PlayerItem player)
    {
        _player = player;
    }

    public void OnEnter()
    {
        _player.Animator.SetBool("Die", true);
    }

    public void OnExit()
    {
        _player.Animator.SetBool("Die", false);
    }

    public void Tick()
    {
        _player.Rigidbody.velocity = Vector2.zero;
    }
}
