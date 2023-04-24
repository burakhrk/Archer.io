using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class EnemyRandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Vector3 centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    bool isSpeedBoost=false;
    AnimationController animationController;

    DotController dotController;

    bool targetFound=false;
    GameObject target;
    private void Awake()
    {
        Init(20);
    }
    public void FightStart()
    {
        agent.speed = 2f;

    }
    public void FightEnd()
    {
        agent.speed = 3.5f;

    }
    public void Init(float _range)
    {
        range = _range;
         agent = GetComponent<NavMeshAgent>();
        centrePoint = transform.position;
        dotController = GetComponent<DotController>(); 
        animationController = GetComponentInChildren<AnimationController>();
    }
    public void SpeedBoost(float boostTime, float multiplier)
    {
        if (isSpeedBoost)
            return;

        StartCoroutine(SpeedBoostNumerator(boostTime, multiplier));
    }
    IEnumerator SpeedBoostNumerator(float time, float multiplier)
    {
        isSpeedBoost = true;
        agent.speed = agent.speed * multiplier;
        yield return new WaitForSeconds(time);
        agent.speed = agent.speed / multiplier;
        isSpeedBoost = false;
    }
    void Update()
    {
        if(target)
        {

            if (target == null)
            {
                targetFound = false;
                target = null;
            }
            else
            {
                agent.SetDestination(target.transform.position);

            }
        }
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            dotController.UpdateAnimation(false, true, false, false);
            if(target)
            {
                targetFound = false;
            }
            if(!targetFound)
            {
                Vector3 point;
                if (RandomPoint(centrePoint, range, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, UnityEngine.Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(point);
                }
            }
            else
            {
                if(target==null)
                {
                    targetFound = false;
                    target = null;
                 }
                else
                {
                    agent.SetDestination(target.transform.position);

                }
            } 
        }
        else
        {
            dotController.UpdateAnimation(true, false, false, false); 
        }
    }
 public void SetFoundTarget(GameObject foundTarget)
    {
        target = foundTarget;
        targetFound = true;
    }

    public void Run(GameObject runTarget)
    {
        target = null;
        targetFound = false;
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: 
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        return;
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }
    }
}