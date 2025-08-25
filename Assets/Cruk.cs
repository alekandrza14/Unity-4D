using Unity.VisualScripting;
using UnityEngine;

public class Cruk : MonoBehaviour
{
    fristPersonControler player;
    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().linearDamping = 1000.0f;
    }
    void Update()
    {
        if (!player) player ??= FindAnyObjectByType<fristPersonControler>();
        Transform Oren = player.transform;
        Vector3 v3 = Oren.position - transform.position;
        Oren.position -= v3 / 15;
    }
}
