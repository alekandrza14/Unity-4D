using UnityEngine;

public class Bullet3 : DamageObject
{
    float timer;
    public GameObject Particles;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(fristPersonControler.Eye * 1300, ForceMode.Force);
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
        timer += Time.deltaTime / 2;
        if (timer > 2.5f) transform.localScale = new Vector3((timer * timer)/2.5f, (timer * timer) / 2.5f, (timer * timer) / 2.5f);
        else if (timer > 5f)
        {
            transform.localScale = new Vector3(40, 40, 40);
        }
        else if (timer > 6f)
        {
            Destroy(gameObject);
        }

    }
}
