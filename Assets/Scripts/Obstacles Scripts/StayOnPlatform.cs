using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlatform : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Stick onto moving ground");
            //other.transform.parent = this.transform;
            target = other.gameObject;
            offset = target.transform.position - transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("goout of moving ground");
            //other.transform.parent = null;
            target = null;
        }
    }
    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }

    }
}
