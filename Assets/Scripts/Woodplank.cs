    using System;
using UnityEngine;

public class Woodplank : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        var position = transform.position + boxCollider.size.y / 2 * Vector3.up;
        var halfExtents = Vector3.Scale(transform.lossyScale, boxCollider.size) / 2;
        var layerMask = LayerMask.GetMask("Objects");

        var colliders = Physics.OverlapBox(
            position,
            halfExtents,
            transform.rotation,
            layerMask);

        foreach (var other in colliders)
        {
            other.GetComponent<Animator>().SetTrigger("Shake");
        }
    }
}
