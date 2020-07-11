using MultiState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditState : IState
{
    private LevelController _levelController;
    private StateMachine _stateMachine;

    public EditState(LevelController levelController)
    {
        _levelController = levelController;

        InitializeStateMachine();
    }

    void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();

        var removeItemState = new RemoveItemState(_levelController);
        var placeItemState = new PlaceItemState(_levelController);

        _stateMachine.AddTransition(removeItemState, placeItemState, () => _levelController.ItemController.SelectedItem != null);
        _stateMachine.AddTransition(placeItemState, removeItemState, () => _levelController.ItemController.SelectedItem == null);

        _stateMachine.SetState(removeItemState);
    }

    public void OnEnter()
    {
        foreach (var placedItem in _levelController.PlacedItems)
        {
            placedItem.EnterEdit();
        }
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        _stateMachine.Tick();
    }
}
