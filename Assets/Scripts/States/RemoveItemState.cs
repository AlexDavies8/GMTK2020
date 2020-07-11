using MultiState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemState : IState
{
    private LevelController _levelController;

    public RemoveItemState(LevelController levelController)
    {
        _levelController = levelController;
    }

    public void OnEnter() { }

    public void OnExit() { }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0) && _levelController.MouseInsideGameArea())
        {
            _levelController.RemoveItem(_levelController.GetMouseGridPosition());
        }
    }
}
