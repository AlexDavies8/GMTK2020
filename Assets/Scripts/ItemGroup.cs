using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Group", menuName = "Item Group")]
public class ItemGroup : ScriptableObject
{
    [SerializeField] private List<CountedItem> _items = new List<CountedItem>();

    public Dictionary<Item, int> GetItemDictionary()
    {
        Dictionary<Item, int> itemDictionary = new Dictionary<Item, int>();
        foreach (var item in _items)
        {
            itemDictionary.Add(item.Item, item.Count);
        }
        return itemDictionary;
    }

    [Serializable]
    struct CountedItem
    {
        public Item Item;
        public int Count;

        public CountedItem(Item item, int count)
        {
            Item = item;
            Count = count;
        }
    }
}
