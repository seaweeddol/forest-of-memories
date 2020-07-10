using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryTextPosition : MonoBehaviour
{
    public Transform player;
    public GameObject memoryContainer;
    public GameObject memory;
    public Collider collider;
    public TreeInfo treeInfo;
    bool m_IsPlayerInRange;

    void Start() {
        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
    }

    void Update (){
        // TODO: make text move with camera aim

        // show memory if user is within range of tree
        if(m_IsPlayerInRange) {
            // canvas.LookAt(player.transform.position);
            memoryContainer.SetActive(true);
        } else {
            memoryContainer.SetActive(false);
        }
    }

    void OnTriggerEnter (Collider other) {
        if(other.transform == player) {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other) {
        if(other.transform == player) {
            m_IsPlayerInRange = false;
        }
    }
}
