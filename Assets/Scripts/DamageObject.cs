using System.Collections;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public float damageAmmount;
   public IEnumerator autoderath()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }
}
