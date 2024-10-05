using UnityEngine;

public class Ammo : MonoBehaviour
{
    public GameObject Particles;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<fristPersonControler>())
        {
            fristPersonControler fpc = collision.GetComponent<fristPersonControler>();
            Instantiate(Particles, transform.position, Quaternion.identity);
            fpc.AddAmmo();
            Destroy(gameObject);
        }
    }
}
