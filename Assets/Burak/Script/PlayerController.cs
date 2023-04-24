using System.Collections;
using System.Collections.Generic;
 
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;
    private Vector3 movement;
    private Vector3 mousePosition;

    bool isSpeedBoost=false;
    DotController dotController;

    private void Awake()
    {
        dotController = GetComponent<DotController>();
    }
    public void SpeedBoost(float boostTime, float multiplier)
    {
        if (isSpeedBoost)
            return;

        StartCoroutine(SpeedBoostNumerator(boostTime,multiplier));
    }
    public void StartFight()
    {
        moveSpeed = 3f;
    }
    public void EndFight()
    {
        moveSpeed = 5f;
    }
    IEnumerator SpeedBoostNumerator(float time , float multiplier)
    {
        isSpeedBoost = true;
        moveSpeed = moveSpeed*multiplier;
         yield return new WaitForSeconds(time);
        moveSpeed = moveSpeed/multiplier;
        isSpeedBoost = false;
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                mousePosition = hit.point;
                mousePosition.y = transform.position.y; // Keep the same height as the player
                movement = (mousePosition - transform.position).normalized; // Move towards the mouse position
            }
        }
    }
  
    private void FixedUpdate()
    {
        if(movement.magnitude>0)
        {
            dotController.UpdateAnimation(true,false,false,false);
        }
        else
        {
            dotController.UpdateAnimation(false, true, false, false);

        }
        transform.position += movement * moveSpeed * Time.fixedDeltaTime;

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}




