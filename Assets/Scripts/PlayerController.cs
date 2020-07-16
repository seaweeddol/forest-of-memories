using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Canvas m_MemoryUI;
    public GameObject m_MemoryJournal;
    public GameObject m_InteractionUI;
    public GameObject m_Camera;
    public AudioSource walkAudio;
    public AudioSource runAudio;
    // TODO: should this be 'text' in range? check facing player?
    public List<GameObject> treesInRange;
 
    private float speed;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator m_Animator;
    private Transform entryNumber;
    private Transform date;
    private Transform sentiment;
    private Transform memory;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator> ();
        // fullMemoryBook = fullMemoryContainer.transform.GetChild(0).gameObject;
        entryNumber = m_MemoryJournal.transform.Find("EntryNumber");
        date = m_MemoryJournal.transform.Find("Date");
        sentiment = m_MemoryJournal.transform.Find("Sentiment");
        memory = m_MemoryJournal.transform.Find("Panel/Memory");
    }
 
    void Update(){
        Cursor.lockState = CursorLockMode.Locked;
        // if memory input UI or memory journal is active, disable mouse look, movement, footstep audio, & walking animation
        if (m_MemoryUI.isActiveAndEnabled || m_MemoryJournal.activeInHierarchy) {
            disableMouseLook();
            controller.Move(new Vector3(0, 0, 0));
            m_Animator.SetBool ("isWalking", false);
            m_Animator.SetBool ("isRunning", false);
            walkAudio.Pause();
            runAudio.Pause();
            m_InteractionUI.SetActive(false);

            if (m_MemoryJournal.activeInHierarchy && Input.GetKeyDown("e")) {
                m_MemoryJournal.SetActive(false);
            }
        } else { 
            if(treesInRange.Count > 0) {
                m_InteractionUI.SetActive(true);
            } else {
                m_InteractionUI.SetActive(false);
            }
            if(Input.GetKeyDown("e")) {
                if(treesInRange.Count > 0) {
                    // TODO: instead of grabbing first tree, grab tree player is looking at
                    TreeInfo treeInfo = treesInRange[0].GetComponent<TreeInfo>();
                    if(!m_MemoryJournal.activeInHierarchy) {
                        m_MemoryJournal.SetActive(true);
                        m_MemoryJournal.GetComponent<AudioSource>().Play();
                        entryNumber.GetComponent<TextMeshProUGUI>().text = "Entry #1";
                        date.GetComponent<TextMeshProUGUI>().text = "Date: " + treeInfo.dateTime;
                        sentiment.GetComponent<TextMeshProUGUI>().text = "Sentiment: " + treeInfo.sentiment;
                        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
                    }
                }
            }

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

