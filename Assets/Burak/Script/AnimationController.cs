using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void SetRun()
    {
        animator.SetBool("Run",true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Die", false);

    }
    public void SetIdle()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Die", false);
    }
    public void SetAttack()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", true);
        animator.SetBool("Die", false);
    }
    public void SetDie()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
        animator.SetBool("Die", true);
    }
}
