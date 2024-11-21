using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    fristPersonControler m;
    // Start is called before the first frame update
    void Start()
    {
      if(FindObjectOfType<fristPersonControler>())  m = FindObjectOfType<fristPersonControler>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-(m.transform.position - transform.position), transform.up);
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
    }
}
