using System.Collections;
using UnityEngine;

public class Bullet2 : DamageObject
{
    public GameObject Particles;
    float timer;
    Vector3 pos;
    Vector3 pos2;
    void Start()
    {
        pos = transform.position;
        pos2 = fristPersonControler.Eye;
        StartCoroutine(autoderath());
        Instantiate(Particles, transform);
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(Particles, transform); 
        if (collision.gameObject.GetComponent<MeshDestroy>())
        {
            collision.gameObject.GetComponent<MeshDestroy>().CollisionUpdate();
        }
    }
    private void Update()
    {
        timer += Time.deltaTime*15;
        transform.localScale = new Vector3(1f, 1f, timer*2);
        if (pos.x + pos.y + pos.z != 0) transform.position = ((pos2 * timer) + pos);
    }
}
