using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 3.0f;

    public Canvas m_MemoryUI;
    public GameObject m_Camera;
 
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator m_Animator;

    void Start(){
        controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator> ();
        // Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update(){
        // if memoryUI is active, disable mouse look, movement, footstep audio, & walking animation
        if (m_MemoryUI.isActiveAndEnabled) {
            disableMouseLook();
            controller.Move(new Vector3(0, 0, 0));
            m_Animator.SetBool ("isWalking", false);
            GetComponent<AudioSource>().Pause();
        } else { 
            // TODO: add run
            enableMouseLook();

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            bool isWalking = checkIsWalking();
            footstepAudio(isWalking);

            moveDirection.y -= Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void enableMouseLook() {
        GetComponent<MouseLook>().enabled = true;
        m_Camera.GetComponent<MouseLook>().enabled = true;
    }

    void disableMouseLook() {
        GetComponent<MouseLook>().enabled = false;
        m_Camera.GetComponent<MouseLook>().enabled = false;
    }

    bool checkIsWalking(){
        bool hasHorizontalInput = !Mathf.Approximately (Input.GetAxis("Horizontal"), 0f);
        bool hasVerticalInput = !Mathf.Approximately (Input.GetAxis("Vertical"), 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("isWalking", isWalking);
        return isWalking;
    }

    void footstepAudio(bool isWalking) {
        // play footstep sounds if player is walking
        bool footsteps = GetComponent<AudioSource>().isPlaying;
        if (isWalking) {
            if (!footsteps) {
                GetComponent<AudioSource>().Play();
            }
        } else {
            GetComponent<AudioSource>().Pause();
        }
    }
 }

