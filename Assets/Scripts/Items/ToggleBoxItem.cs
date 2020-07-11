using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBoxItem : PoweredItem
{
    [SerializeField] private Sprite _activeSprite = null;
    [SerializeField] private Sprite _inactiveSprite = null;

    SpriteRenderer _spriteRenderer;
    Collider2D _collider;

    public override void Place()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    [ContextMenu("Power")]
    public override void Power()
    {
        _spriteRenderer.sprite = _inactiveSprite;
        _collider.enabled = false;
    }

    [ContextMenu("Unpower")]
    public override void Unpower()
    {
        _spriteRenderer.sprite = _activeSprite;
        _collider.enabled = true;
    }
}
