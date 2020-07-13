using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4.0f;

    public Canvas m_MemoryUI;
    public GameObject m_Camera;
 
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator m_Animator;

    // Use this for initialization
    void Start(){
        controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator> ();
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    // Update is called once per frame
    void Update(){
        // disable mouse look, movement, footstep audio, & walking animation if memoryUI is active
        if (m_MemoryUI.isActiveAndEnabled) {
            controller.Move(new Vector3(0, 0, 0));
            m_Animator.SetBool ("isWalking", false);
            GetComponent<MouseLook>().enabled = false;
            m_Camera.GetComponent<MouseLook>().enabled = false;
            GetComponent<AudioSource>().Pause();
        }

        if(!m_MemoryUI.isActiveAndEnabled) {
            GetComponent<MouseLook>().enabled = true;
            m_Camera.GetComponent<MouseLook>().enabled = true;

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            bool hasHorizontalInput = !Mathf.Approximately (Input.GetAxis("Horizontal"), 0f);
            bool hasVerticalInput = !Mathf.Approximately (Input.GetAxis("Vertical"), 0f);
            bool isWalking = hasHorizontalInput || hasVerticalInput;
            m_Animator.SetBool ("isWalking", isWalking);

            // play footstep sounds if player is walking
            bool footsteps = GetComponent<AudioSource>().isPlaying;
            if (isWalking) {
                if (!footsteps) {
                    GetComponent<AudioSource>().Play();
                }
            } else {
                GetComponent<AudioSource>().Pause();
            }

            
            // can use this if block for running
            //  if (Input.GetButton("Jump"))
            //      moveDirection.y = jumpSpeed;

            moveDirection.y -= Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }

    }
 }