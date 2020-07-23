using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public UserInterface m_UserInterface;
    public GameObject m_MainMenuUI;
    public GameObject m_NewGameUI;
    public GameObject m_SaveGameUI;
    public GameObject m_LoadGameUI;
    public GameObject m_ExploreUI;
    public GameObject m_MemoryUI;
    public InputField m_InputField;
    public GameObject m_MemoryJournal;
    public GameObject m_InteractionUI;
    public GameObject m_ControlsUI;
    public GameObject m_GameOptionsUI;
    public AudioSource walkAudio;
    public AudioSource runAudio;
    public List<GameObject> treesInRange;
    public Material glowMaterial;
    public Material treeMaterial;
 
    private float speed;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private GameObject m_Camera;
    private Animator m_Animator;
    private Transform entryNumber;
    private Transform date;
    private Transform strongestTone;
    private Transform allTones;
    private Transform memory;

    void Start(){
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
        m_UserInterface = m_UserInterface.transform.GetComponent<UserInterface>();
        m_Camera = transform.Find("Kira/Camera").gameObject;
        m_Animator = GetComponent<Animator> ();
        entryNumber = m_MemoryJournal.transform.Find("EntryNumber");
        date = m_MemoryJournal.transform.Find("Date");
        strongestTone = m_MemoryJournal.transform.Find("StrongestTone");
        allTones = m_MemoryJournal.transform.Find("AllTones");
        memory = m_MemoryJournal.transform.Find("Panel/Memory");
    }
 
    void Update(){
        // if any UI is active, disable mouse look, movement, audio, & animation, and listen for ESC
        if (m_MainMenuUI.activeInHierarchy || m_MemoryUI.activeInHierarchy || m_MemoryJournal.activeInHierarchy || m_ControlsUI.activeInHierarchy || m_GameOptionsUI.activeInHierarchy || m_NewGameUI.activeInHierarchy || m_SaveGameUI.activeInHierarchy || m_LoadGameUI.activeInHierarchy || m_ExploreUI.activeInHierarchy) {
            disableMovement();

            if(!m_MainMenuUI.activeInHierarchy && Input.GetKeyDown("escape")) {
                m_GameOptionsUI.SetActive(false);
                m_ControlsUI.SetActive(false);
                m_MemoryUI.SetActive(false);
                m_MemoryJournal.SetActive(false);
                m_InputField.text = "";
            }

            if (m_MemoryJournal.activeInHierarchy && (Input.GetKeyDown("e") || Input.GetMouseButtonDown(0))) {
                m_MemoryJournal.SetActive(false);
            }
        } else { 
            if(Input.GetKeyDown("escape")) {
                // m_GameOptionsUI.SetActive(true);
                m_UserInterface.ShowGameOptionsUI();
                return;
            } else if(Input.GetKeyDown("space")) {
                m_MemoryUI.SetActive(true);
                m_InputField.ActivateInputField();
                return;
            }

            Cursor.visible = false;

            checkTreesInRange();

            // set speed depending on if player is holding down shift
            speed = Input.GetKey("left shift") ? 7.0f : 3.5f;

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
        Cursor.visible = true;
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

            // find tree most within player's view
            foreach(GameObject currentTree in treesInRange) {
                currentTree.GetComponent<Renderer>().material = treeMaterial;
                forward = (transform.TransformDirection(Vector3.forward)).normalized;
                toTree = (currentTree.transform.position - transform.position).normalized;

                float angle = Vector3.Dot(toTree, forward);
                if(angle > minAngle) {
                    minAngle = angle;
                    tree = currentTree;
                }
            }

            // activate InteractionUI & tree glow, and listen for "e" key press if tree is in player view
            if (minAngle >= 0.8 && treesInRange.Contains(tree)) {
                m_InteractionUI.SetActive(true);
                tree.GetComponent<Renderer>().material = glowMaterial;

                if(Input.GetKeyDown("e")) {
                    displayMemoryJournal(tree);
                }
            } else {
                m_InteractionUI.SetActive(false);
                tree.GetComponent<Renderer>().material = treeMaterial;
            }    
        }
    }

    // display memory journal with info from the selected tree
    void displayMemoryJournal(GameObject tree) {
        TreeInfo treeInfo = tree.GetComponent<TreeInfo>();
        m_MemoryJournal.SetActive(true);
        m_MemoryJournal.GetComponent<AudioSource>().Play();
        entryNumber.GetComponent<TextMeshProUGUI>().text = "Entry #" + treeInfo.entryNum;
        date.GetComponent<TextMeshProUGUI>().text = "Date: " + treeInfo.dateTime;
        strongestTone.GetComponent<TextMeshProUGUI>().text = "Strongest Sentiment: " + treeInfo.strongestTone;
        allTones.GetComponent<TextMeshProUGUI>().text = "All Sentiments: " + string.Join(", ", treeInfo.allTones);
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

