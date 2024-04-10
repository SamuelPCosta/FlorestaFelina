using UnityEngine;

[ExecuteAlways]
public class BoxCastController : MonoBehaviour
{
    [Header("Boxcast options")]
    public float maxDistance = 1f;
    public bool draw = false;

    private Vector3 dimensions = new Vector3(1f, 1f, .2f);

    void Update()
    {
        
    }

    public bool checkBoxCast(int layer)
    {

        int layerMask = 1 << layer;

        return
          Physics.BoxCast(
            transform.position,
            dimensions * 0.5f,
            transform.forward,
            Quaternion.identity,
            maxDistance,
            layerMask
          );
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
                Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
            }
        }
    }
}