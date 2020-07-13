using MultiState;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerItem : PhysicsItem
{
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private string _killTag = "Kill";
    [SerializeField] private string _exitTag = "Exit";
    [SerializeField] private Animator _animator = null;
    [SerializeField] private LayerMask _groundLayer = default;
    [SerializeField] private AudioClip _deathSound = null;

    public float MovementSpeed => _movementSpeed;
    public int Direction { get; set; } = 1;
    public LevelController LevelController { get; private set; }
    public Animator Animator => _animator;

    public bool OnGround { get; set; }
    public bool Climbing { get; set; }

    StateMachine _stateMachine;
    bool _playing;
    bool _dead;

    WalkState _walkState;

    private void Update()
    {
        if (_playing) _stateMachine.Tick();

        transform.rotation = Quaternion.Euler(0f, Mathf.Abs(Direction - 1) * 90f, 0f);
    }

    public override void Place()
    {
        base.Place();

        LevelController = FindObjectOfType<LevelController>();

        _stateMachine = new StateMachine();

        _walkState = new WalkState(this);
        var fallState = new FallState(this);
        var climbState = new ClimbState(this);
        var dieState = new DieState(this);

        _stateMachine.AddTransition(_walkState, fallState, () => !OnGround);
        _stateMachine.AddTransition(fallState, _walkState, () => OnGround);
        _stateMachine.AddTransition(_walkState, climbState, () => Climbing);
        _stateMachine.AddTransition(climbState, _walkState, () => !Climbing);

        _stateMachine.AddAnyTransition(dieState, () => _dead);
    }

    public override void EnterPlay()
    {
        base.EnterPlay();
        _playing = true;
    }

    public override void EnterEdit()
    {
        base.EnterEdit();
        _playing = false;

        _dead = false;
        _stateMachine.SetState(_walkState);
        Direction = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_killTag))
        {
            _dead = true;
            SoundClipPlayer.PlayClip(_deathSound);
        }
        else if (collision.CompareTag(_exitTag) && !_dead)
        {
            LevelController.WinLevel();
        }
    }

    List<RaycastHit2D> raycastResults = new List<RaycastHit2D>();
    public RaycastHit2D Raycast(Vector2 offset)
    {
        ContactFilter2D filter = new ContactFilter2D() { useTriggers = false, layerMask = _groundLayer.value, useLayerMask = true };
        
        Physics2D.CircleCast(transform.position, 0.01f, offset, filter, raycastResults, offset.magnitude);

        return raycastResults.FirstOrDefault();
    }
}
