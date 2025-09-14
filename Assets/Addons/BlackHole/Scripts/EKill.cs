using System.Collections;
using UnityEngine;

public class EKill : MonoBehaviour
{
    public IEnumerator FatalDiying(Collider other)
    {
        yield return new WaitForSeconds(0.5f); 
        other.GetComponent<fristPersonControler>().reloadScene();
        yield return null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.GetComponent<fristPersonControler>())
        {
            fristPersonControler.interes = -1;
            if (Random.Range(0,3)>1)
            {
                StartCoroutine(FatalDiying(other));
            }
        }
    }
}
