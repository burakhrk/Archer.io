using System.Collections;
using System.Collections.Generic;
 
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;

    private Vector3 movement;
    private Vector3 mousePosition;

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
        transform.position += movement * moveSpeed * Time.fixedDeltaTime;

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}




