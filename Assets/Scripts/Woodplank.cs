using UnityEngine;

public class Woodplank : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        // The OverlapBox checks if there are any colliders within the target box area:
        // https://docs.unity3d.com/ScriptReference/Physics.OverlapBox.html

        // We position the box check right at the edge of the plank
        Vector3 position = transform.position + boxCollider.size.y / 2 * Vector3.up;

        // We use the collider size to get the correct size of the box we are checking
        // But we need to multiply by the scale, to transform it from local to world
        Vector3 halfExtents = Vector3.Scale(transform.lossyScale, boxCollider.size) / 2;

        // To optimize our search, we can filter the checks to a specific layer.
        // For this we created the "Objects" layer, and assigned the objects on top of the plank to it.
        int layerMask = LayerMask.GetMask("Objects");

        Collider[] colliders = Physics.OverlapBox(
            position,
            halfExtents,
            transform.rotation,
            layerMask);

        foreach (var other in colliders)
        {
            // To perform the shake visual, we use a simple animation that changes de rotation of the object
            // fast enough to seem like it is shaking
            other.GetComponent<Animator>().SetTrigger("Shake");
        }
    }
}
