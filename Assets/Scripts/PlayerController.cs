using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 3.0f;

    public Canvas m_MemoryUI;
    public Canvas m_MemoryJournal;
    public GameObject m_Camera;
    public AudioSource walkAudio;
    public AudioSource runAudio;
 
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator m_Animator;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator> ();
        // Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update(){
        Cursor.lockState = CursorLockMode.Locked;
        // if memoryUI is active, disable mouse look, movement, footstep audio, & walking animation
        if (m_MemoryUI.isActiveAndEnabled || m_MemoryJournal.isActiveAndEnabled) {
            disableMouseLook();
            controller.Move(new Vector3(0, 0, 0));
            m_Animator.SetBool ("isWalking", false);
            m_Animator.SetBool ("isRunning", false);
            walkAudio.Pause();
            runAudio.Pause();
        } else { 
            // TODO: add run
            if (Input.GetKey("left shift")) {
                speed = 6.0f;
            } else {
                speed = 2.5f;
            }

            enableMouseLook();

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            bool isMoving = checkIsMoving(speed);
            footstepAudio(isMoving);

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

    // check if player is moving and set animations
    bool checkIsMoving(float speed){
        bool hasHorizontalInput = !Mathf.Approximately (Input.GetAxis("Horizontal"), 0f);
        bool hasVerticalInput = !Mathf.Approximately (Input.GetAxis("Vertical"), 0f);
        bool isMoving = hasHorizontalInput || hasVerticalInput;
        if(speed == 6.0f) {
            m_Animator.SetBool ("isRunning", isMoving);
            m_Animator.SetBool ("isWalking", false);
        } else {
            m_Animator.SetBool ("isWalking", isMoving);
            m_Animator.SetBool ("isRunning", false);
        }
        return isMoving;
    }

    // determine if audio should play, and what type of audio
    void footstepAudio(bool isMoving) {
        bool walkFootsteps = walkAudio.isPlaying;
        bool runFootsteps = runAudio.isPlaying;
        if (isMoving) {
            if(speed == 2.5f) {
                if (!walkFootsteps) {
                    walkAudio.Play();
                    runAudio.Pause();
                } 
            } else {
                if (!runFootsteps) {
                    runAudio.Play();
                    walkAudio.Pause();
                }
            }
        } else {
            walkAudio.Pause();
            runAudio.Pause();
        }
    }
 }

