
using UnityEngine;

public class Bird : Enemy
{
    public Rigidbody body;
    fristPersonControler player;
    MultyTransform hideplayer;
    public MultyObject pos;
    public float hp = 221.1f;
    public GameObject Particles;
    public GameObject Particles2;

    private void OnCollisionStay(Collision collision)
    {
        GameObject OBJ = collision.gameObject;
        if (OBJ.GetComponent<DamageObject>())
        {
            if (fristPersonControler.current.perck == GunPerck.money)
            {
                fristPersonControler.current.ammo += OBJ.GetComponent<DamageObject>().damageAmmount / 20;
            }
            hp -= OBJ.GetComponent<DamageObject>().damageAmmount;
            if (hp < 0)
            {
                Instantiate(Particles2, transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
            Instantiate(Particles, transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<fristPersonControler>())
        {
            player = other.GetComponent<fristPersonControler>();
        }
    }

    void Start()
    {
     //   Instantiate(Particles, transform.position, Quaternion.identity);
        hideplayer ??= FindFirstObjectByType<MultyTransform>();
    }

    
    void Update()
    {
        if (player) 
        {
            if (!pos.hide)
            {
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                body.AddForce(transform.forward / 3, ForceMode.VelocityChange);
            }
            if (pos.W_Position > hideplayer.W_Position) pos.W_Position -= 0.01f;
            if (pos.W_Position < hideplayer.W_Position) pos.W_Position += 0.01f;
            if (pos.H_Position > hideplayer.H_Position) pos.H_Position -= 0.01f;
            if (pos.H_Position < hideplayer.H_Position) pos.H_Position += 0.01f;

        }
    }
}
