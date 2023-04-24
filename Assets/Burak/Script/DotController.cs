using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    EnemyRandomMovement EnemyRandom;
    PlayerController playerController;
   [SerializeField] bool isPlayer = false;
    public List<Dot> dots = new List<Dot>();
    public Material groupMaterial;
  [SerializeField]  DotManager dotManager;
    BotDetector detector;

    bool isFigthing = false;
    [SerializeField] DotController FightingObj;
    private void Awake()
    {
        if (!isPlayer)
        {
            detector = GetComponentInChildren<BotDetector>();
            EnemyRandom = GetComponent<EnemyRandomMovement>();
        }
        else
            playerController = GetComponent<PlayerController>();

        Init(groupMaterial);
    }
    public void Init(Material material)
    {
        groupMaterial = material;
    }
    
    public void UpdateList(List<Dot> newDots)
    {

        dots = newDots;

        if(dots.Count==0)
        {
            GameObject go = gameObject;
            Destroy(go);
           if (isPlayer)
            {
                Debug.LogError("Lose");
            }
        }

        if(!isPlayer)
        detector.UpdateCapsuleRadius(dots.Count);
    }
    public void UpdateAnimation(bool isRun, bool isIdle, bool isAttack, bool isDie)
    {
        foreach (var item in dots)
        {
            item.UpdateAnimation(isRun, isIdle, isAttack, isDie);
        }
    }
    public void StartFight(DotController target)
    {
        if (!isPlayer)
            EnemyRandom.FightStart();
        else
            playerController.StartFight();

        isFigthing = true;
        for (int i = 0; i < dots.Count-1; i++)
        {
            if (target == null||FightingObj==null|| (target.dots.Count <= i))
                break;

                 dots[i].Attack(target.dots[i].gameObject);  
        }

     }
    public void EndFight(DotController target)
    {
        if (!isPlayer)
            EnemyRandom.FightEnd();

        else
            playerController.EndFight();

            FightingObj = null;
        isFigthing = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isFigthing)
            return;

         if (other.gameObject.GetComponent<DotController>())
        {
            if(isPlayer)
            {
                FightingObj = other.gameObject.GetComponent<DotController>();

                if (FightingObj.dots.Count == dots.Count)
                    return;

                StartFight(FightingObj);
                Debug.Log("fight");
            }
            else
            {
                FightingObj = other.gameObject.GetComponent<DotController>();

                if (FightingObj.dots.Count == dots.Count)
                    return;

                StartFight(FightingObj); 
                Debug.Log("fight");
            }
        }
     }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DotController>()==FightingObj)
        {
            if (isPlayer)
            {
                EndFight(FightingObj);
            }
            else
            {
                EndFight(FightingObj);
            } 
        }
    }
}
