using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemGroup _startItems = null;

    [SerializeField] private Camera _camera = null;

    public Dictionary<Item, int> Items { get; private set; } = new Dictionary<Item, int>();
    public Action ItemsChanged { get; set; }
    public Item SelectedItem { get; private set; }
    public Action SelectionChanged { get; set; }

    private void Awake()
    {
        if (!_camera) _camera = Camera.main;
    }

    private void Start()
    {
        if (_startItems) Items = _startItems.GetItemDictionary();
        ItemsChanged.Invoke();
    }

    public void SelectItem(Item item)
    {
        if (item != null && !Items.ContainsKey(item)) return;

        if (SelectedItem == item) SelectedItem = null;
        else SelectedItem = item;

        SelectionChanged?.Invoke();
    }

    public void AddItem(Item item)
    {
        if (Items.ContainsKey(item)) Items[item]++;
        else Items.Add(item, 1);

        ItemsChanged.Invoke();
    }
    public bool RemoveItem(Item item)
    {
        if (item == null) return false;
        if (!Items.ContainsKey(item)) return false;

        Items[item]--;

        if (Items[item] == 0)
        {
            Items.Remove(item);
            SelectedItem = null;
        }

        ItemsChanged();

        return true;
    }
}
