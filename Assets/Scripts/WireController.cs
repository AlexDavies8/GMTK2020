using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private GameObject _wirePrefab = null;
    [SerializeField] private Transform _wireContainer = null;

    public List<Wire> Wires { get; private set; } = new List<Wire>();

    public void AddWire(PowerSourceItem source, PoweredItem destination)
    {
        var wireGO = Instantiate(_wirePrefab, _wireContainer);
        wireGO.transform.position = Vector2.zero;

        var wire = wireGO.GetComponent<Wire>();

        wire.Source = source;
        wire.Destination = destination;

        wire.UpdateLineRenderer();
    }
}
