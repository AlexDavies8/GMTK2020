using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public PowerSourceItem Source { get; set; }
    public PoweredItem Destination { get; set; }

    private LineRenderer _lineRenderer;

    public Action OnRemove;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void UpdateLineRenderer()
    {
        Vector3[] positions = new Vector3[2];

        positions[0] = (Vector2)Source.GridPosition;
        positions[1] = (Vector2)Destination.GridPosition;

        _lineRenderer.SetPositions(positions);
    }

    public void Remove()
    {
        OnRemove?.Invoke();
        Destroy(gameObject);
    }
}
