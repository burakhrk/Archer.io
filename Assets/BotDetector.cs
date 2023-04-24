using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDetector : MonoBehaviour
{
    CapsuleCollider capsuleCollider;
    EnemyRandomMovement RandomMovement;
    DotController dotController;
    private void Awake()
    {
        
        capsuleCollider = GetComponent<CapsuleCollider>();
        RandomMovement = GetComponentInParent<EnemyRandomMovement>();
        dotController = RandomMovement.gameObject.GetComponent<DotController>();
    }
    public void UpdateCapsuleRadius(int count )
    {
        if (count == 0)
            return;
       
        capsuleCollider.radius = count*2;

       if(capsuleCollider.radius>=40)
        {
            capsuleCollider.radius = 40;

        }
    }
    
 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<RandomMovement>())
        {
            Debug.Log("Dot found setting target");
            RandomMovement.SetFoundTarget(other.gameObject);
        }

        DotController dotControllerOther;
        if (other.gameObject.GetComponent<DotController>())
        {
            dotControllerOther = other.gameObject.GetComponent<DotController>();

            if (dotController.dots.Count > dotControllerOther.dots.Count)
            {
                RandomMovement.SetFoundTarget(other.gameObject);
            }
            else
            {
                RandomMovement.Run(other.gameObject);
            }
        }
    }
}
