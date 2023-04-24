using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Dot : MonoBehaviour
{
   [SerializeField] AnimationController animationController;
   [SerializeField] SkinnedMeshRenderer SkinnedMesh;

    PlaceInCircle placeInCircle;
    [SerializeField] DotDrop dotDropPrefab;

    [SerializeField] Weapon weapon;
  
    public void PlaceInPos(Vector3 targetPos,float time,Vector3 rotation,Material material)
    {
      transform.SetPositionAndRotation(targetPos, Quaternion.Euler(rotation));
        placeInCircle = GetComponentInParent<PlaceInCircle>();

        DotCaptured(material);
         //  transform.DOMove(targetPos,time);
    }
    public void InitFreeDot()
    {
         gameObject.AddComponent<RandomMovement>().Init(10);
    }
    public void Attack(GameObject target)
    {
        weapon.Target(target);
    }
    public void Die()
    {
        Debug.LogError("dieee");
        placeInCircle.RemoveSpecific(this);

          GameObject go = Instantiate(dotDropPrefab.gameObject);
          go.transform.position = transform.position;
          go.GetComponent<DotDrop>().Init(GetComponentInParent<PlaceInCircle>());
    }
    public void UpdateAnimation(bool isRun, bool isIdle, bool isAttack, bool isDie)
    {
        if (isRun)
            animationController.SetRun();

        if (isIdle)
            animationController.SetIdle();

        if (isAttack)
            animationController.SetAttack();

        if (isDie)
            animationController.SetDie();
    }
    public void DotCaptured(Material material)
    {
        SkinnedMesh.material = material;
    }
}
