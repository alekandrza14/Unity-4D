using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Mathematics.math;
using static Unity.VisualScripting.Member;
public enum GunPerck
{
    none,money
}
[System.Serializable]
public class Gun
{
    public GameObject Bullet;
    public Transform Spawner;
    public Animator GunAinmator;
    public GameObject Model;
    public float timeout;
    public float ammo = 84.2f;
    public float MaxAmmo = 84.2f;
    public float res = 0.6f;
    public Text ammoText;
    public GunPerck perck;
}

public class fristPersonControler : MonoBehaviour
{
    public int ne_nu_tolko_sosem_imbu_ne_delay;
    public Rigidbody rb;
    public GameObject[] g;
    public Gun[] Guns;
    public Gun[] LockGuns;
    public static Vector3 Eye;
    public static Gun current;
    public GameObject GameOwer;
    public static float interes = 8;
    public int currentGun;
    public Text InteresCounter;
    public Text Vin;
    public Text PositionInfo;
    public Text InteractData;
    public Text TimerCounter;
    public AudioSource sourece;
    public AudioSource взмах;
    public GameObject FlyLink;
    public GameObject JumpLink;
    float timer;
    bool Interact;
    bool reload;
    [SerializeField] MultyTransform multyTransform;
    float fmod2(float a, float b)
    {
        float c = frac(abs(a / b)) * abs(b);
        if (a < 0)
        {
            c = -c + b;
        }
        return c;
    }
    bool zevnul;
    public void AddAmmo()
    {
        Gun hands = Guns[currentGun];
        hands.ammo += hands.MaxAmmo / 5;
        hands.ammo += (float)Math.Round( UnityEngine.Random.Range(0, hands.MaxAmmo / 5),1);
    }
    // Start is called before the first frame update
    void Start()
    {
        current = Guns[currentGun];
        for (int i=0;i<LockGuns.Length; i++)
        {
            if (VarSave.GetInt("Gun" + i)==1)
            {
                List<Gun> guns = Guns.ToList();
                guns.Add(LockGuns[i]);
                Guns = guns.ToArray();
            }
        }
        InteresSet(21);
    }
    IEnumerator Shoot()
    {
        reload = true;
        Gun hands = Guns[currentGun];
        if (hands.ammo > 0) InteresSet(15);
        if (hands.ammo > 0)
        {
            Instantiate(hands.Bullet, hands.Spawner.position, hands.Spawner.rotation);
            hands.ammo -= hands.res;
        }

        if (hands.perck != GunPerck.money) if (hands.ammo > hands.MaxAmmo * 2)
            {

                hands.ammo = hands.MaxAmmo * 2;
            }
        rb.AddForce(-3 * Eye, ForceMode.Impulse);
        if (hands.ammo < hands.MaxAmmo) yield return new WaitForSeconds(hands.timeout);
        if (hands.ammo > hands.MaxAmmo) yield return new WaitForSeconds(hands.timeout / 2);
        reload = false;
    }

    private static void AmmoColorText(Gun hands)
    {
        if (hands.perck == GunPerck.none)
        {
            if (hands.ammo > hands.MaxAmmo)
            {

                hands.ammoText.color = Color.red;
                hands.GunAinmator.speed = 2;
            }
            else
            {
                hands.ammoText.color = Color.white;
                hands.GunAinmator.speed = 1;
            }
        }
        if (hands.perck == GunPerck.money)
        {
            if (hands.ammo > hands.MaxAmmo)
            {

                hands.ammoText.color = Color.yellow;
                hands.GunAinmator.speed = 2;
            }
            else
            {
                hands.ammoText.color = Color.red;
                hands.GunAinmator.speed = 1;
            }
        }
    }

    float obs(float x, float max)
    {
        if (x<0)
        {
            return max + x;
        }
        return x;
    }
    IEnumerator SwapWearon(float well)
    {

        InteresSet(15);
        reload = true;
        int iwell = well > 0 ? 1 : -1;
        currentGun += iwell;
        currentGun %= Guns.Length;
        currentGun = (int)obs(currentGun, Guns.Length);
        yield return new WaitForSeconds(0.1f);
        reload = false;
    }
    public void InteresSet(float x)
    {
       if(interes<x) interes = x;
    }
    public void reloadScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void TryNow()
    {
        InteresSet(999);
        GameOwer.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        TimerCounter.text = "" + (int)(timer / 60) + ":" + System.Math.Round((timer % 60), 1);
        current = Guns[currentGun];
        AmmoColorText(current);
        PositionInfo.text = "W : " + Math.Round(multyTransform.W_Position,1);
        if (FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length<=0)
        {
            Vin.text = "All Enemye Destroyed";
        }
        else
        {
            timer += Time.deltaTime;
        }
        interes -= Time.deltaTime;
        if (interes < 0)
        {
            GameOwer.SetActive(true); Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //   GameOwer.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (interes<5)
        {
            if (!zevnul) 
            {
                sourece.Play();
                zevnul = true;
            }
            InteresCounter.color = new Color(0, 0, 0, 0.25f);
        }
        else
        {
            zevnul = false;
            InteresCounter.color = new Color(1, 1, 1, 1f);
        }
        if (Interact)
        {
            InteractData.text = "Teleport";
            FlyLink.SetActive(true);
            JumpLink.SetActive(false);
        }
        else
        {

            InteractData.text = "Shoot";
            FlyLink.SetActive(false);
            JumpLink.SetActive(true);
        }
        //Mouse ScrollWheel
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) if (!reload) StartCoroutine(SwapWearon(Input.GetAxisRaw("Mouse ScrollWheel")));
        if (Guns[currentGun].perck == GunPerck.none) Guns[currentGun].ammoText.text = Math.Round(Guns[currentGun].ammo, 1).ToString() + " / " + Math.Round(Guns[currentGun].MaxAmmo, 1).ToString();
        if (Guns[currentGun].perck == GunPerck.money) Guns[currentGun].ammoText.text = Math.Round(Guns[currentGun].ammo, 1).ToString() + " $ " + Math.Round(Guns[currentGun].MaxAmmo, 1).ToString();
        InteresCounter.text = "IP : " + Math.Round(interes,1);
        Eye = g[1].transform.forward;
        Ray r = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(r, out RaycastHit hit))
        {
            if (hit.distance <= 1.5f && Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * (50 * Time.deltaTime), ForceMode.Impulse);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && Interact)
        {
            взмах.Play();
        }
        if (Input.GetKey(KeyCode.Space) && Interact)
        {
            rb.AddForce(Vector3.up * (50 * Time.deltaTime), ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InteresSet(8);
            currentGun = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InteresSet(8);
            currentGun = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InteresSet(8);
            currentGun = 2;
        }
        if (GameOwer.activeSelf == false)
        {
            if (!Interact)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {

                    Guns[currentGun].GunAinmator.SetFloat("Shoot", 3);

                }
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (!reload) StartCoroutine(Shoot());
                }
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {

                    Guns[currentGun].GunAinmator.SetFloat("Shoot", 0);

                }
            }
            else
            {
                Ray rcam = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Input.GetKeyDown(KeyCode.Mouse0)) if (Physics.Raycast(rcam, out RaycastHit hit2))
                    {
                        transform.position = hit2.point + Vector3.up;
                        InteresSet(14);
                    }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            InteresSet(12);
            interes++;
            Interact = !Interact;
        }
        if (GameOwer.activeSelf == false)
        {
            if (!Interact)
            {

                Cursor.lockState = CursorLockMode.Locked;
                g[0].transform.Rotate(0, Input.GetAxisRaw("Mouse X") * (400f * Time.deltaTime), 0);
                g[1].transform.Rotate(-Input.GetAxisRaw("Mouse Y") * (400f * Time.deltaTime), 0, 0); 
            //  if (Input.GetAxisRaw("Mouse X")+ Input.GetAxisRaw("Mouse Y")!=0)  interes = 10;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        for (int i = 0; i < Guns.Length;i++)
        {
            if (i != currentGun)
            {
                Guns[i].Model.SetActive(false);
            }
            else
            {
                Guns[i].Model.SetActive(true);
            }
        }
        Guns[currentGun].GunAinmator.SetFloat("Move", Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical")+ Input.GetAxisRaw("Vertical1")+ Input.GetAxisRaw("Horizontal1") * 2);

        if (GameOwer.activeSelf == false)
        {
            if ((rb.linearVelocity.x + rb.linearVelocity.z) <= 1) rb.MovePosition(((transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical")) / 6) + transform.position);
            multyTransform.W_Position += Input.GetAxisRaw("Vertical1") * Time.deltaTime * 3;
            multyTransform.H_Position += Input.GetAxisRaw("Horizontal1") * Time.deltaTime * 3;
            if (Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Vertical1") + Input.GetAxisRaw("Horizontal1")!=0)
            {
             //   interes = 8;
            }
        }


    }
}
