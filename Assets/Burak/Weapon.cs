using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;

public class Weapon : MonoBehaviour
{
    public void Target(GameObject targetObj)
    {
        transform.SetParent(null);
        Time.timeScale = 0.5f;
        transform.DODynamicLookAt(targetObj.transform.position,1f);
        transform.DOMove(targetObj.transform.position, 1f).OnComplete(() => targetObj.GetComponent<Dot>().Die());
    }
}
