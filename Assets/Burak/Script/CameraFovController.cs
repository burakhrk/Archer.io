using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CameraFovController : MonoBehaviour
{
    [SerializeField] float multiplier=0.5f;
   [SerializeField] CinemachineVirtualCamera cinemachine;
    Tween tween;
    public void UpdateFov(float numberOfObjects)
    { 
          //  cinemachine.m_Lens.FieldOfView = 45+(multiplier*numberOfObjects);
         if(tween!=null)
        {
            tween.Kill();
        }
        float angle = cinemachine.m_Lens.FieldOfView;
         tween= DOTween.To(() => angle, x => cinemachine.m_Lens.FieldOfView = x, 45 + (multiplier * numberOfObjects), 0.5f);
    }
}
