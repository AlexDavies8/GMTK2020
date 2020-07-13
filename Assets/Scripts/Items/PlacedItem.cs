using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedItem : MonoBehaviour
{
    [SerializeField] private bool _removable = true;
    [SerializeField] private bool _placeOnAwake = false;
    [SerializeField] private Item _item = null;
    [SerializeField] private AudioClip _placeSound = null;

    public Item Item { get => _item; set => _item = value; }
    public Vector2Int GridPosition 
    { 
        get => new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f));
        set => transform.position = value + Vector2.one * 0.5f;
    }
    public bool Removable => _removable;
    protected bool PlaceOnAwake => _placeOnAwake;
    public Action OnRemove { get; set; }
    protected bool Playing { get; private set; }
    bool _removed = false;

    private void Awake()
    {
        if (!Removable || _placeOnAwake)
        {
            FindObjectOfType<LevelController>().AddItem(this);
        }
    }

    public virtual void Place() { if (!_placeOnAwake && Removable) SoundClipPlayer.PlayClip(_placeSound); }
    public virtual void EnterPlay() { Playing = true; }
    public virtual void EnterEdit() { Playing = false; }
    public virtual void Remove()
    {
        if (_removed) return;

        _removed = true;

        SoundClipPlayer.PlayClip(_placeSound);

        OnRemove?.Invoke();
        Destroy(gameObject);
    }
}
