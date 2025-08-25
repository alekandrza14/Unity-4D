using UnityEngine;
[ExecuteAlways]
[ExecuteInEditMode]
public class RandomRotate : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(0,Random.Range(-180,180),0);
    }
}
