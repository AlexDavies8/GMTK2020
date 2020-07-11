using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemControllerView : MonoBehaviour
{
    [SerializeField] private ItemController _controller = null;
    [SerializeField] private Transform _itemViewContainer = null;
    [SerializeField] private GameObject _itemViewPrefab = null;
    [SerializeField] private RectTransform _selectionTransform = null;

    List<ItemView> _itemViews = new List<ItemView>();

    private void Awake()
    {
        _controller.ItemsChanged += OnItemsChanged;
        _controller.SelectionChanged += () => UpdateSelection(_controller.Items.Keys.ToList());
    }

    void OnItemsChanged()
    {
        var items = _controller.Items.Keys.ToList();
        for (int i = 0; i < items.Count; i++)
        {
            var itemView = GetView(i);

            Action<Item> onSelect = (item) => {
                _controller.SelectItem(item);

                UpdateSelection(items);
            };

            itemView.SetItem(items[i], onSelect);
            itemView.SetCount(_controller.Items[items[i]]);
        }

        if (_itemViews.Count > items.Count)
        {
            int difference = _itemViews.Count - items.Count;
            for (int i = 0; i < difference; i++)
            {
                Destroy(_itemViews[items.Count].gameObject);
                _itemViews.RemoveAt(items.Count);
            }
        }

        UpdateSelection(items);
    }

    void UpdateSelection(List<Item> items)
    {
        if (_controller.SelectedItem == null)
        {
            _selectionTransform.gameObject.SetActive(false);
            return;
        }

        for (int i = 0; i < items.Count; i++)
        {
            var item = items[i];

            if (_controller.SelectedItem == item)
            {
                _selectionTransform.gameObject.SetActive(true);
                _selectionTransform.position = GetView(i).transform.position;

                return;
            }
        }
    }

    ItemView GetView(int index)
    {
        if (index < _itemViews.Count) return _itemViews[index];

        var viewGO = Instantiate(_itemViewPrefab, _itemViewContainer);
        var itemView = viewGO.GetComponent<ItemView>();

        _itemViews.Add(itemView);

        return itemView;
    }
}
