using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDrop : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;
        if(col.CompareTag("Player"))
        {
            col.transform.GetComponentInParent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.CompareTag("Player"))
        {
            col.transform.GetComponent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }
    }
}
