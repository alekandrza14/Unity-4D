using UnityEngine;

public class Bullet4 : DamageObject
{
    float timer;
    public GameObject Particles;
    Vector3 pos;
    Vector3 pos2;
    void Start()
    {
        pos = transform.position;
        pos2 = fristPersonControler.Eye;
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
        timer += Time.deltaTime *15f;
         transform.localScale = new Vector3((fristPersonControler.interes/5), (fristPersonControler.interes / 5), (fristPersonControler.interes / 5));
        if (timer > 260f)
        {
            Destroy(gameObject);
        }
        transform.position = ((pos2 * timer) + pos);
    }
}
