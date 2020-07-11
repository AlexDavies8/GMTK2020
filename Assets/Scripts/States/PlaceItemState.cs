using MultiState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemState : IState
{
    private LevelController _levelController;

    public PlaceItemState(LevelController levelController)
    {
        _levelController = levelController;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0) && _levelController.MouseInsideGameArea())
        {
            var itemController = _levelController.ItemController;
            _levelController.PlaceItem(itemController.SelectedItem);
        }
    }
}
