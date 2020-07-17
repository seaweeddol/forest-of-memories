using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject m_MemoryUI;
    public InputField m_InputField;
    public GameObject m_MemoryJournal;
    public GameObject m_InteractionUI;
    public GameObject m_ControlsUI;
    public AudioSource walkAudio;
    public AudioSource runAudio;
    public List<GameObject> treesInRange;
 
    private float speed;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private GameObject m_Camera;
    private Animator m_Animator;
    private Transform entryNumber;
    private Transform date;
    private Transform sentiment;
    private Transform memory;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        m_Camera = transform.Find("Kira/Camera").gameObject;
        m_Animator = GetComponent<Animator> ();
        entryNumber = m_MemoryJournal.transform.Find("EntryNumber");
        date = m_MemoryJournal.transform.Find("Date");
        sentiment = m_MemoryJournal.transform.Find("Sentiment");
        memory = m_MemoryJournal.transform.Find("Panel/Memory");
    }
 
    void Update(){
        Cursor.lockState = CursorLockMode.Locked;

        // if any UI is active, disable mouse look, movement, audio, & animation, and listen for ESC
        if (m_MemoryUI.activeInHierarchy || m_MemoryJournal.activeInHierarchy || m_ControlsUI.activeInHierarchy) {
            disableMovement();

            if(Input.GetKeyDown("escape")) {
                m_ControlsUI.SetActive(false);
                m_MemoryUI.SetActive(false);
                m_MemoryJournal.SetActive(false);
                m_InputField.text = "";
            }

            if (m_MemoryJournal.activeInHierarchy && Input.GetKeyDown("e")) {
                m_MemoryJournal.SetActive(false);
            }
        } else { 
            if(Input.GetKeyDown("escape")) {
                m_ControlsUI.SetActive(true);
                return;
            } else if(Input.GetKeyDown("space")) {
                m_MemoryUI.SetActive(true);
                m_InputField.ActivateInputField();
                return;
            }

            checkTreesInRange();

            // set speed depending on if player is holding down shift
            speed = Input.GetKey("left shift") ? 6.0f : 2.5f;

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

    void disableMovement() {
        disableMouseLook();
        controller.Move(new Vector3(0, 0, 0));
        m_Animator.SetBool ("isWalking", false);
        m_Animator.SetBool ("isRunning", false);
        walkAudio.Pause();
        runAudio.Pause();
        m_InteractionUI.SetActive(false);
    }

    void checkTreesInRange() {
        // check if any trees are in range
        if(treesInRange.Count > 0) {
            // get minimum angle to compare against
            GameObject tree = treesInRange[0];
            Vector3 forward = (transform.TransformDirection(Vector3.forward)).normalized;
            Vector3 toTree = (tree.transform.position - transform.position).normalized;
            float minAngle = Vector3.Dot(toTree, forward);

            // check each tree angle against minAngle
            foreach(GameObject currentTree in treesInRange) {
                currentTree.GetComponent<Renderer>().material.color = Color.white;
                forward = (transform.TransformDirection(Vector3.forward)).normalized;
                toTree = (currentTree.transform.position - transform.position).normalized;

                float angle = Vector3.Dot(toTree, forward);
                if(angle > minAngle) {
                    minAngle = angle;
                    tree = currentTree;
                }
            }

            // activate InteractionUI & listen for "e" key press if tree is in player view
            if (minAngle >= 0.8) {
                m_InteractionUI.SetActive(true);
                tree.GetComponent<Renderer>().material.color = new Color(0.97f, 0.71f, 0.36f);

                if(Input.GetKeyDown("e")) {
                    displayMemoryJournal(tree);
                }
            } else {
                m_InteractionUI.SetActive(false);
                tree.GetComponent<Renderer>().material.color = Color.white;
            }    
        }
    }

    void displayMemoryJournal(GameObject tree) {
        TreeInfo treeInfo = tree.GetComponent<TreeInfo>();
        m_MemoryJournal.SetActive(true);
        m_MemoryJournal.GetComponent<AudioSource>().Play();
        entryNumber.GetComponent<TextMeshProUGUI>().text = "Entry #1";
        date.GetComponent<TextMeshProUGUI>().text = "Date: " + treeInfo.dateTime;
        sentiment.GetComponent<TextMeshProUGUI>().text = "Sentiment: " + treeInfo.sentiment;
        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
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

