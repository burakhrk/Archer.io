using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] GameObject dropPrefab;
    [SerializeField] Transform dropParent;
    [SerializeField] int dropCount=15;
    [SerializeField] float dropArea=20f;

    private void Awake()
    {
        for (int i = 0; i < dropCount; i++)
        {
            GameObject go = Instantiate(dropPrefab,dropParent);
            go.transform.position = GenerateRandomPosition();
        }
    }

    public Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-dropArea, dropArea);
         float z = Random.Range(-dropArea, dropArea);

        Vector3 randomPosition = new Vector3(x, 1.5f, z);

        return randomPosition;
    }

}
