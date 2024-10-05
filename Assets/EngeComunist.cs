using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngeComunist : Enemy
{
    public Rigidbody body;
    fristPersonControler player;
    MultyTransform hideplayer;
    public MultyObject pos;
    public float hp = 221.1f;
    public GameObject Particles;
    public GameObject Particles2;
    public GameObject Particles3;
    public GameObject Prise;
    public GameObject Bullet;
    Vector3 StartPosition;
    bool reload;
    bool reload2;
    private void OnCollisionStay(Collision collision)
    {

        GameObject OBJ = collision.gameObject;
        if (OBJ.GetComponent<DamageObject>())
        {
            if (fristPersonControler.current.perck == GunPerck.money)
            {
                fristPersonControler.current.ammo += OBJ.GetComponent<DamageObject>().damageAmmount/20;
            }
            hp -= OBJ.GetComponent<DamageObject>().damageAmmount;
            if (hp < 0)
            {
                MultyObject mo = Instantiate(Prise, transform.position, Quaternion.identity).GetComponent<MultyObject>();
                mo.W_Position = pos.W_Position;
                mo.H_Position = pos.H_Position;
                Instantiate(Particles2, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
           if(!player) player = FindFirstObjectByType<fristPersonControler>();
            Instantiate(Particles, transform.position, Quaternion.identity);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<fristPersonControler>())
        {
            if (!player) player = other.GetComponent<fristPersonControler>();
        }
    }
    fristPersonControler fpc;
    void Start()
    {
        StartPosition = transform.position;
        fpc = FindObjectOfType<fristPersonControler>();
        //   Instantiate(Particles, transform.position, Quaternion.identity);
        hideplayer ??= FindFirstObjectByType<MultyTransform>();
        InvokeRepeating("Telep", 2*60,20);
    }
    IEnumerator Shoot()
    {
        reload = true;
        fristPersonControler.interes *= 0.98f;
        Instantiate(Bullet, fpc.transform.position, Quaternion.LookRotation(-(fpc.transform.position - transform.position)));
        yield return new WaitForSeconds(0.1f);
        reload = false;
    }
    void Telep()
    {
        StartCoroutine(ToSpawn());
    }
    IEnumerator ToSpawn()
    {
        reload2 = true;
        body.linearVelocity = Vector3.zero;
        Instantiate(Particles3, fpc.transform.position, Quaternion.identity);
        transform.position = StartPosition;
        yield return new WaitForSeconds(20);
        reload2 = false;
    }

    void Update()
    {
        if (player)
        {
            if (!pos.hide)
            {
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                body.AddForce(transform.forward / 10, ForceMode.VelocityChange);
                if (!reload) StartCoroutine(Shoot());

                if (pos.W_Position > hideplayer.W_Position) pos.W_Position -= 0.01f;
                if (pos.W_Position < hideplayer.W_Position) pos.W_Position += 0.01f;
                if (pos.H_Position > hideplayer.H_Position) pos.H_Position -= 0.01f;
                if (pos.H_Position < hideplayer.H_Position) pos.H_Position += 0.01f;
            }
            if (!reload2) StartCoroutine(ToSpawn());
        }



    }
}
