using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiState;

public class PlayState : IState
{
    private LevelController _levelController;

    public PlayState(LevelController levelController)
    {
        _levelController = levelController;
    }

    public void OnEnter()
    {
        foreach (var placedItem in _levelController.PlacedItems)
        {
            placedItem.EnterPlay();
        }
    }

    public void OnExit()
    {

    }

    public void Tick()
    {

    }
}
