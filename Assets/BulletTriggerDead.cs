using UnityEngine;

public class BulletTriggerDead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageObject>())
        {
            fristPersonControler.interes = -100;
        }
    }
}
