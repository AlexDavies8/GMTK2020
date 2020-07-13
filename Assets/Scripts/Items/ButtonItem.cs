using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonItem : PowerSourceItem
{
    [SerializeField] private Sprite _normalSprite = null;
    [SerializeField] private Sprite _pressedSprite = null;

    SpriteRenderer _spriteRenderer;

    public override void Place()
    {
        base.Place();

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision) => Press();
    private void OnCollisionExit2D(Collision2D collision) => Unpress();
    private void OnTriggerEnter2D(Collider2D collision) => Press();
    private void OnTriggerExit2D(Collider2D collision) => Unpress();

    private void Press()
    {
        _spriteRenderer.sprite = _pressedSprite;
        Power();
    }

    private void Unpress()
    {
        _spriteRenderer.sprite = _normalSprite;
        Unpower();
    }
}
