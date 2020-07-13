using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private Sprite _icon = null;
    [SerializeField] private GameObject _prefab = null;
    [SerializeField] private bool _solid = true;

    public Sprite Icon { get => _icon; set => _icon = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public bool Solid => _solid;
}
