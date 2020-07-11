using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPlacer : PlacedItem
{
    [SerializeField] private TileBase _tile = null;

    Vector3Int _gridPosition;
    Tilemap _tilemap;

    public override void Place()
    {
        base.Place();

        _tilemap = GetComponentInParent<Tilemap>();

        _gridPosition = new Vector3Int(GridPosition.x, GridPosition.y, 0);

        _tilemap.SetTile(_gridPosition, _tile);
    }

    public override void Remove()
    {
        _tilemap.SetTile(_gridPosition, null);

        base.Remove();
    }
}
