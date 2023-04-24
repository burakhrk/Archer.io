using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDrop : MonoBehaviour
{
    [SerializeField] float speedMultiplier=1.5f;
    [SerializeField] float boostTimer=3f;
    private void OnTriggerEnter(Collider other)
    {
        GameObject col = other.gameObject;
        if (col.CompareTag("Player"))
        {
            col.transform.GetComponent<PlayerController>().SpeedBoost(boostTimer, speedMultiplier);
             Destroy(gameObject);
        }
        if (col.CompareTag("Enemy"))
        {
            col.transform.GetComponent<EnemyRandomMovement>().SpeedBoost(boostTimer, speedMultiplier);
            Destroy(gameObject);
        }
    }
}
