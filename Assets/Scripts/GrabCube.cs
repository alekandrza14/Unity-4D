using UnityEngine;

public class GrabCube : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {

        GameObject OBJ = collision.gameObject;
        if (OBJ.GetComponent<DamageObject>())
        {
            Destroy(OBJ);
        }
    }
}
