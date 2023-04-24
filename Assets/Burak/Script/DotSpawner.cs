using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpawner : MonoBehaviour
{
    [SerializeField] Transform dropParent; 
    [SerializeField] GameObject dotPrefab;
    [SerializeField] float spawnTimer;
    [SerializeField] int dropCount = 5; 
    private void Awake()
    {
        Spawn();
    }
    void Spawn()
    {
        for (int i = 0; i < dropCount; i++)
        {
            GameObject go = Instantiate(dotPrefab, dropParent);
            go.transform.position = GenerateRandomPosition();
            go.GetComponent<Dot>().InitFreeDot();
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
        float x = Random.Range(-50  , 50);
        float z = Random.Range(-50, 50);

        Vector3 randomPosition = new Vector3(x, 0.5f, z); 
        return randomPosition;
    }
}
