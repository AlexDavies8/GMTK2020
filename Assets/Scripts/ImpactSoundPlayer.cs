using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip _impactClip = null;
    [SerializeField] private string _impactTag = "Ground"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_impactTag) || (collision.attachedRigidbody.velocity.magnitude < 0.01f && GetComponentInParent<Rigidbody2D>().velocity.magnitude > 0.01f)) SoundClipPlayer.PlayClip(_impactClip, 0.8f);
    }
}
