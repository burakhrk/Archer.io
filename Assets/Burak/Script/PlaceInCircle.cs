using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInCircle : MonoBehaviour
{
    public GameObject objectPrefab; // The object to place in a circle
    public int numberOfObjects; // The number of objects to place
    //  public Vector2 minMaxRadius;
    public float RadiusMultiplier=0.8f;
     public float DotMoveTime=0.5f;
    List<Dot> _objList = new List<Dot>();
    CapsuleCollider circleCollider;

   [SerializeField] GameManager gameManager;
    DotController dotController;
    
    private void Awake()
    {
        circleCollider = GetComponent<CapsuleCollider>();
        dotController = GetComponent<DotController>();
 
    }
    private void Start()
    {
        Add(); 
    }
    void UpdateCircleCollider(float radius)
    {
        circleCollider.radius = radius;

        dotController.UpdateList(_objList);
    }

    public void CreateObjects(Transform spawnPos)
    {
        var remaining = numberOfObjects - _objList.Count;
         // var radius = Mathf.Lerp(minMaxRadius.x, minMaxRadius.y, percent);
         var radius = numberOfObjects*RadiusMultiplier;
        for (int i = 0; i < remaining; i++)
        {
            GameObject obj;
            if (spawnPos!=null)
            {
                obj = Instantiate(objectPrefab, spawnPos);
            }
            else
            {
                  obj = Instantiate(objectPrefab,transform);
            }
           
            _objList.Add(obj.GetComponent<Dot>());
        }

        for (int i = 0; i < _objList.Count; i++)
        {
            var obj = _objList[i];
            float angle = i * Mathf.PI * 2f / numberOfObjects;
            Vector3 newPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius + transform.position;
            obj.transform.SetParent(transform);
            var dot = obj.GetComponent<Dot>();
            dot.PlaceInPos(newPosition, DotMoveTime,transform.rotation.eulerAngles,dotController.groupMaterial);
        }


        UpdateCircleCollider(radius);

        if(gameObject.CompareTag("Player"))
        gameManager.UpdatePlayerCount(numberOfObjects);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Add();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Remove();
        }
    }

    public void SpawnWithPos(Transform spawnPos)
    {
        numberOfObjects++;
        CreateObjects(spawnPos);
    }
    [Button]
    public void Add()
    {
        numberOfObjects++;
        CreateObjects(null);
    }

    public void RemoveSpecific(Dot dot)
    {
        Debug.LogError(dot +" spe");
        _objList.Remove(dot);
         
        Destroy(dot.gameObject);
         numberOfObjects--;
        CreateObjects(null);
    }

    [Button]

    public void Remove()
    {
        var obj = _objList[_objList.Count - 1];
        Destroy(obj);
        _objList.RemoveAt(_objList.Count - 1);
        numberOfObjects--;
        CreateObjects(null);
    }
}
