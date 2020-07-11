using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiState;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private ItemController _itemController = null;
    [SerializeField] private Transform _placedItemContainer = null;
    [SerializeField] private Rect _gameArea = new Rect();

    public ItemController ItemController => _itemController;
    public Transform PlacedItemContainer => _placedItemContainer;

    public List<PlacedItem> PlacedItems { get; private set; } = new List<PlacedItem>();
    public bool Playing { get; set; }

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
        foreach (var existingItem in PlacedItems)
        {
            if (existingItem.GridPosition == placePosition) return;
        }

        if (!itemController.RemoveItem(item)) return;

        var itemGO = Instantiate(item.Prefab, PlacedItemContainer);
        var placedItem = itemGO.GetComponent<PlacedItem>();

        placedItem.GridPosition = placePosition;
        placedItem.Item = item;
        placedItem.Place();

        PlacedItems.Add(placedItem);
    }

    public void RemoveItem(Vector2Int gridPosition)
    {
        for (int i = 0; i < PlacedItems.Count; i++)
        {
            if (PlacedItems[i].GridPosition == gridPosition)
            {
                var placedItem = PlacedItems[i];

                ItemController.AddItem(placedItem.Item);

                placedItem.Remove();
                PlacedItems.RemoveAt(i);

                return;
            }
        }
    }

    public Vector2Int GetMouseGridPosition()
    {
        var placePosition = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition) - Vector2.one * 0.5f;
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
}
