using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorItem : PlacedItem
{
    [SerializeField] private int _direction = 1;

    public override void Place()
    {
        base.Place();

        var animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Time.time % animator.GetCurrentAnimatorStateInfo(0).length);

        transform.rotation = Quaternion.Euler(0, Mathf.Abs(_direction - 1) * 90, 0);
    }

    Collider2D[] _overlapResults = new Collider2D[8];
    private void FixedUpdate()
    {
        if (Playing)
        {
            Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + Vector2.up * 0.625f, new Vector2(0.99f, 0.25f), 0f, _overlapResults);

            for(int i = 0; i < _overlapResults.Length; i++)
            {
                var collider = _overlapResults[i];

                _overlapResults[i] = null;

                if (!collider || collider.attachedRigidbody.bodyType == RigidbodyType2D.Static) continue;

                collider.attachedRigidbody.velocity = Vector2.zero;

                if (collider.attachedRigidbody.bodyType != RigidbodyType2D.Static)
                {
                    collider.attachedRigidbody.MovePosition(collider.attachedRigidbody.position + (Vector2.right * _direction + Vector2.down * collider.attachedRigidbody.gravityScale) * Time.fixedDeltaTime);
                }
            }
        }
    }
}
