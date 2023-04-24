using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] Transform dropParent;

    [SerializeField] int spawnTimer;

    [SerializeField] GameObject speedBoosterPrefab;
    [SerializeField] int speedDropCount = 5;

     
    [SerializeField] float dropArea=20f;

    private void Awake()
    {
        Spawn();
    }
    void Spawn()
    { 
        for (int i = 0; i < speedDropCount; i++)
        {
            GameObject go = Instantiate(speedBoosterPrefab, dropParent);
            go.transform.position = GenerateRandomPosition();
        }
        StartCoroutine(SpawnTimer());
    }
    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);
        Spawn();
    }
    public Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-dropArea, dropArea);
         float z = Random.Range(-dropArea, dropArea);

        Vector3 randomPosition = new Vector3(x, 0, z);

        return randomPosition;
    }

}
