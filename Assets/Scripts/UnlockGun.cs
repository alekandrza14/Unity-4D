using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnlockGun : MonoBehaviour
{
    public GameObject Particles;
    public int IDUnlock;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<fristPersonControler>())
        {
            fristPersonControler fpc = collision.GetComponent<fristPersonControler>();
            Instantiate(Particles, transform.position, Quaternion.identity);
            List<Gun> guns = fpc.Guns.ToList();
            guns.Add(fpc.LockGuns[IDUnlock]);
            VarSave.SetInt("Gun" + IDUnlock, 1);
            fpc.Guns = guns.ToArray();
            Destroy(gameObject);
        }
    }
}
