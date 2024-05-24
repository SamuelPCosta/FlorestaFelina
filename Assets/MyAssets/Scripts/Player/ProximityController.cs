using UnityEngine;

[ExecuteAlways]
public class ProximityController : MonoBehaviour
{
    [Header("Boxcast options")]
    public float maxDistance = 1f;
    public float barrierDistance = 5f;
    public bool draw = false;

    private Vector3 dimensions = new Vector3(1f, 1f, .2f);

    public Collider checkProximity(int layer)
    {
        float distance = maxDistance;
        if (LayerMask.LayerToName(layer) == "Barrier")
            distance = barrierDistance;

        int layerMask = 1 << layer;

        Collider collider = checkBoxcast(layerMask, distance);
        if (collider != null)
            return collider;
        else
            return collider = checkOverlap(layerMask);
    }

    private Collider checkBoxcast(int layerMask, float distance)
    {
        RaycastHit hit = new RaycastHit();
        Physics.BoxCast(
            transform.position,
            dimensions * 0.5f,
            transform.forward,
            out hit,
            Quaternion.identity,
            distance,
            layerMask
        );

        return hit.collider;
    }

    private Collider checkOverlap(int layerMask)
    {
        Collider[] colliders = Physics.OverlapBox(
            transform.position,
            dimensions * 0.5f,
            Quaternion.identity,
            layerMask
        );

        return colliders.Length > 0 ? colliders[0] : null;
    }

    private void OnDrawGizmos()
    {
        if (draw)
        {
            RaycastHit hit;

            bool isHit = Physics.BoxCast(transform.position, dimensions * 0.5f, transform.forward, out hit,
                transform.rotation, maxDistance);
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
                Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, dimensions);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            }
        }
    }
}