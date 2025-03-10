
using UnityEngine;

public class teserog : Enemy
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
   

    void Start()
    {
     //   Instantiate(Particles, transform.position, Quaternion.identity);
      if(FindObjectOfType<MultyTransform>())  hideplayer = FindObjectOfType<MultyTransform>();
    }

    
    void Update()
    {
       
    }
}
