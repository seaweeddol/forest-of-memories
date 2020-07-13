using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
     public bool controllable = false;
 
     public float speed = 4.0f;
     public float gravity = 20.0f;
 
     private Vector3 moveDirection = Vector3.zero;
     private CharacterController controller;
 
     // Use this for initialization
     void Start()
     {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
     }
 
     // Update is called once per frame
     void Update()
     {
        // Cursor.lockState = CursorLockMode.Locked;

         if (controller.isGrounded && controllable)
         {
             moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
             moveDirection = transform.TransformDirection(moveDirection);
             moveDirection *= speed;
            
            //  if (Input.GetButton("Jump"))
            //      moveDirection.y = jumpSpeed;
 
         }
             moveDirection.y -= gravity * Time.deltaTime;
             controller.Move(moveDirection * Time.deltaTime);
     }
 }