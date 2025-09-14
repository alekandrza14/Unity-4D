using UnityEngine;

public class GivePerk : MonoBehaviour
{
    public GameObject Particles;
    public string perckname;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            VarSave.SetInt(perckname,1);
            Instantiate(Particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
