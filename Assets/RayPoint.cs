using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RayPoint : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        Ray r = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(r,out hit))
        {
            target.position = hit.point;
        }
    }
}
