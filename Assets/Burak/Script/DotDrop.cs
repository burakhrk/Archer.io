using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDrop : MonoBehaviour
{
    PlaceInCircle self;
    SphereCollider sphereCollider;
    public void Init(PlaceInCircle selff)
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        self = selff;
        Invoke("Enable",0.5f);
    }
    void Enable()
    {
        sphereCollider.enabled = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        /*
        GameObject col = collision.gameObject;
        if(col.CompareTag("Player"))
        {
            col.transform.GetComponentInParent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        
        GameObject col = other.gameObject;
        /*
        if (col.GetComponent<PlaceInCircle>() == self)
            return;
        */
        if (col.CompareTag("Player"))
        {
            col.transform.GetComponent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }

        if (col.CompareTag("Enemy"))
        {
            col.transform.GetComponent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }

    }
}
