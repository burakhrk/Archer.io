using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Dot : MonoBehaviour
{
    public void PlaceInPos(Vector3 targetPos,float time)
    {
        transform.SetPositionAndRotation(targetPos, Quaternion.identity);
        //  transform.DOMove(targetPos,time);
    }
}
