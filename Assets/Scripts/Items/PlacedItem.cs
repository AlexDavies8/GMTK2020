using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedItem : MonoBehaviour
{
    public Item Item { get; set; }
    public Vector2Int GridPosition 
    { 
        get => new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f));
        set => transform.position = value + Vector2.one * 0.5f;
    }

    public virtual void Place() { }
    public virtual void EnterPlay() { }
    public virtual void EnterEdit() { }
    public virtual void Remove() 
    {
        Destroy(gameObject);
    }
}
