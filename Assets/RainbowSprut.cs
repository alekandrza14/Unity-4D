using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class RainbowSprut : Enemy
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
                fristPersonControler.current.ammo += OBJ.GetComponent<DamageObject>().damageAmmount / 20;
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
    fristPersonControler fpc;
    void Start()
    {
        StartPosition = transform.position;
        fpc = FindObjectOfType<fristPersonControler>();
        //   Instantiate(Particles, transform.position, Quaternion.identity);
        hideplayer ??= FindFirstObjectByType<MultyTransform>();
    }
    IEnumerator Shoot()
    {
        reload = true;
        Instantiate(Bullet, fpc.transform.position, Quaternion.LookRotation(-(fpc.transform.position - transform.position)));
        fristPersonControler.interes *= 0.5f;
        yield return new WaitForSeconds(5);
        reload = false;
    }
    IEnumerator ToSpawn()
    {
        reload2 = true;
        body.linearVelocity = Vector3.zero;
        Instantiate(Particles3, transform.position, Quaternion.identity);
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
                if (!reload) StartCoroutine(Shoot());
                if (!reload2) StartCoroutine(ToSpawn());
                if (pos.W_Position > hideplayer.W_Position) pos.W_Position -= 0.01f;
                if (pos.W_Position < hideplayer.W_Position) pos.W_Position += 0.01f;
                if (pos.H_Position > hideplayer.H_Position) pos.H_Position -= 0.01f;
                if (pos.H_Position < hideplayer.H_Position) pos.H_Position += 0.01f;
            }
        }


    }
}
