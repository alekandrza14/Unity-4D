using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedSpace : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            other.transform.localScale /= 4;
            other.GetComponent<Rigidbody>().linearDamping *= 8;
            Camera[] cameras = other.GetComponentsInChildren<Camera>();
            foreach (Camera c in cameras)
            {
                c.farClipPlane /= 2;
                c.nearClipPlane /= 2;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            StartCoroutine(OutPressedSpace(other));
        }
    }
    IEnumerator OutPressedSpace(Collider other)
    {
        yield return new WaitForSeconds(1);
        other.transform.localScale *= 4;
        other.GetComponent<Rigidbody>().linearDamping /= 8;
        Camera[] cameras = other.GetComponentsInChildren<Camera>();
        foreach (Camera c in cameras)
        {
            c.farClipPlane *= 2;
            c.nearClipPlane *= 2;
        }
    }
}
