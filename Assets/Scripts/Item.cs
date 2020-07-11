using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private string _id = "";
    [SerializeField] private Sprite _icon = null;
    [SerializeField] private GameObject _prefab = null;

    public string ID { get => _id; set => _id = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
}
