using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WireItem : PoweredItem
{
    [SerializeField] private PowerSourceItem _source = null;
    [SerializeField] private PoweredItem _destination = null;

    public PowerSourceItem Source { get => _source; private set => _source = value; }
    public PoweredItem Destination { get => _destination; private set => _destination = value; }

    private LevelController _levelController;
    private LineRenderer _lineRenderer;
    private EdgeCollider2D _edgeCollider;

    bool linked;

    public override void Place()
    {
        _levelController = FindObjectOfType<LevelController>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _edgeCollider = GetComponentInChildren<EdgeCollider2D>();

        if (PlaceOnAwake || !Removable)
        {
            AssignSource();
            AssignDestination();

            linked = true;
            _edgeCollider.SetPoints(new Vector2[] { Source.AttachTransform.position - transform.position, Destination.AttachTransform.position - transform.position }.ToList());
            _lineRenderer.SetPositions(new Vector3[] { Source.AttachTransform.position, Destination.AttachTransform.position });
            return;
        }

        TryAssignMissing(GridPosition);
    }

    private void Update()
    {
        if (linked) return;

        if (Source) _lineRenderer.SetPositions(new Vector3[] { Source.AttachTransform.position, _levelController.GetTrueMousePosition() });
        else _lineRenderer.SetPositions(new Vector3[] { Destination.AttachTransform.position, _levelController.GetTrueMousePosition() });

        if (Input.GetMouseButtonUp(0))
        {
            TryAssignMissing(_levelController.GetMouseGridPosition());
        }
    }

    void TryAssignMissing(Vector2Int position)
    {
        bool hasSource = Source;
        bool hasDestination = Destination;

        if (!hasSource) TryAssignSource(position);
        if (!hasDestination) TryAssignDestination(position);

        if (Source == hasSource && Destination == hasDestination)
        {
            _levelController.RemoveItem(this);
            return;
        }

        if (Source && Destination)
        {
            linked = true;
            _edgeCollider.SetPoints(new Vector2[] { Source.AttachTransform.position - transform.position, Destination.AttachTransform.position - transform.position }.ToList());
            _lineRenderer.SetPositions(new Vector3[] { Source.AttachTransform.position, Destination.AttachTransform.position });
        }
    }

    void TryAssignSource(Vector2Int position)
    {
        Source = _levelController.GetPlacedItemsAt(position).Where(item => item.Item.Solid).FirstOrDefault() as PowerSourceItem;
        AssignSource();
    }

    void AssignSource()
    {
        if (Source != null)
        {
            Source.OnRemove += () =>
            {
                Source = null;
                Destination = null;
                Remove();
            };
            Source.AddWire(this);
        }
    }

    void TryAssignDestination(Vector2Int position)
    {
        Destination = _levelController.GetPlacedItemsAt(position).Where(item => item.Item.Solid).FirstOrDefault() as PoweredItem;
        AssignDestination();
    }

    void AssignDestination()
    {
        if (Destination != null)
        {
            Destination.OnRemove += () =>
            {
                Source = null;
                Destination = null;
                Remove();
            };
        }
    }

    public override void Power()
    {
        Destination.Power();
    }

    public override void Unpower()
    {
        Destination.Unpower();
    }
}
