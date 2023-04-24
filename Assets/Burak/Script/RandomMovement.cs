 using System.Collections;
  using UnityEngine;
using UnityEngine.AI; //important

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Vector3 centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area


    AnimationController animationController;
    public void Init(float _range)
    {
        range = _range;
        gameObject.AddComponent<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();
        centrePoint = transform.position;

        animationController= GetComponentInChildren<AnimationController>();
    }
    void SetRun()
    {
        animationController.SetRun(); 
    }
    void SetIdle()
    {
        animationController.SetIdle (); 
    }
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            SetIdle();
            Vector3 point;
            if (RandomPoint(centrePoint, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
        else
        {
            SetRun();
        }

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
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<PlaceInCircle>().SpawnWithPos(transform);
            Destroy(gameObject);
        }
    }
}