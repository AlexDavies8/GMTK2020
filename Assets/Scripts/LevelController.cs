using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiState;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Rect _gameArea = new Rect();
    [SerializeField] private ItemController _itemController = null;
    [SerializeField] private Transform _placedItemContainer = null;

    public ItemController ItemController => _itemController;
    public Transform PlacedItemContainer => _placedItemContainer;

    public List<PlacedItem> PlacedItems { get; private set; } = new List<PlacedItem>();
    public bool Playing { get; set; }
    public bool WireMode { get; set; }

    StateMachine _stateMachine;

    private void Awake()
    {
        if (!_camera) _camera = Camera.main;

        InitializeStateMachine();
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    void InitializeStateMachine()
    {
        _stateMachine = new StateMachine();

        var playState = new PlayState(this);
        var editState = new EditState(this);

        _stateMachine.AddTransition(playState, editState, () => !Playing);
        _stateMachine.AddTransition(editState, playState, () => Playing);

        _stateMachine.SetState(editState);
    }

    public void PlaceItem(Item item)
    {
        var itemController = ItemController;

        Vector2Int placePosition = GetMouseGridPosition();
        if (!CanPlaceItem(placePosition)) return;

        if (!itemController.RemoveItem(item)) return;

        var itemGO = Instantiate(item.Prefab, PlacedItemContainer);
        var placedItem = itemGO.GetComponent<PlacedItem>();

        placedItem.GridPosition = placePosition;
        placedItem.Item = item;

        AddItem(placedItem);
    }

    public void AddItem(PlacedItem placedItem)
    {
        placedItem.Place();
        PlacedItems.Add(placedItem);
    }

    public void RemoveItem(Vector2Int gridPosition)
    {
        var placedItem = GetPlacedItemAt(gridPosition);

        if (placedItem == null || !placedItem.Removable) return;

        ItemController.AddItem(placedItem.Item);

        placedItem.Remove();
        PlacedItems.Remove(placedItem);
    }

    public Vector2 GetTrueMousePosition()
    {
        return (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector2Int GetMouseGridPosition()
    {
        var placePosition = GetTrueMousePosition() - Vector2.one * 0.5f;
        return new Vector2Int(Mathf.RoundToInt(placePosition.x), Mathf.RoundToInt(placePosition.y));
    }

    public Vector2 GetMousePosition()
    {
        return GetMouseGridPosition() + Vector2.one * 0.5f;
    }

    public bool MouseInsideGameArea()
    {
        return _gameArea.Contains(GetMousePosition());
    }

    public PlacedItem GetPlacedItemAt(Vector2Int gridPosition)
    {
        for (int i = 0; i < PlacedItems.Count; i++)
        {
            if (PlacedItems[i].GridPosition == gridPosition) return PlacedItems[i];
        }

        return null;
    }

    public bool CanPlaceItem(Vector2Int gridPosition)
    {
        if (GetPlacedItemAt(gridPosition) != null) return false;

        var collider = Physics2D.OverlapPoint(gridPosition + Vector2.one * 0.5f);
        if (collider) return false;

        return true;
    }
}
