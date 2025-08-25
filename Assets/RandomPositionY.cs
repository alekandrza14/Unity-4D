using UnityEngine;

[ExecuteAlways]
[ExecuteInEditMode]
public class RandomPositionY : MonoBehaviour
{
    void Start()
    {
        transform.position += new Vector3(0, Random.Range(-3, 3), 0);
    }
}
