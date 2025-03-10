using UnityEngine;

public class Bullet : DamageObject
{
    
    public GameObject Particles;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(fristPersonControler.Eye * 1300, ForceMode.Force);
        StartCoroutine(autoderath());
        Instantiate(Particles,transform);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(Particles, transform);
        if (collision.gameObject.GetComponent<MeshDestroy>())
        {
            collision.gameObject.GetComponent<MeshDestroy>().CollisionUpdate();
        }
    }

}
