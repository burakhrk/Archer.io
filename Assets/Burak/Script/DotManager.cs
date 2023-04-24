using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotManager : MonoBehaviour
{
    public List<Material> dotColorList = new List<Material>();

    public GameObject botPrefab;
    [SerializeField] int botCount;
    [SerializeField] float dropArea = 20f;

    private void Start()
    {
        InstantiateBots();
    }
    void InstantiateBots()
    {
        for (int i = 0; i < botCount; i++)
        {
            GameObject go = Instantiate(botPrefab);
            go.GetComponent<DotController>().Init(GetColor());

            go.transform.position = GenerateRandomPosition();
        }
      
    }
    public Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-dropArea, dropArea);
        float z = Random.Range(-dropArea, dropArea);

        Vector3 randomPosition = new Vector3(x, 0, z);

        return randomPosition;
    }
    public Material GetColor()
    {
        Material material = dotColorList[Random.Range(0,dotColorList.Count)];
         return material;
    }


}
