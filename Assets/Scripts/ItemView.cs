using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Button _button = null;
    [SerializeField] private Text _countText = null;
    [SerializeField] private RectTransform _countBackground = null;
    [SerializeField] private Image _image = null;

    public void SetItem(Item item, Action<Item> onClick)
    {
        _image.sprite = item.Icon;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => onClick.Invoke(item));
    }

    public void SetCount(int count)
    {
        _countText.text = count.ToString();

        // Calculate Text Width
        int width = 0;
        foreach (var character in _countText.text)
        {
            if (character == '1') width += 3;
            else width += 5;
        }
        _countBackground.sizeDelta = new Vector2(width, 6);
    }
}
