﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    public Canvas m_MemoryUI;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;


    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
    }

    void FixedUpdate ()
    {
        // TODO: if memory UI is visible, player should not be able to walk
        if(!m_MemoryUI.isActiveAndEnabled) {
            float horizontal = Input.GetAxis ("Horizontal");
            float vertical = Input.GetAxis ("Vertical");
            
            m_Movement.Set(horizontal, 0f, vertical);
            m_Movement.Normalize ();

            bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
            bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
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

            Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation (desiredForward);
        }
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
}