using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryTextPosition : MonoBehaviour
{
    public Transform player;
    public GameObject memory;
    public GameObject tree;
    
    private TreeInfo treeInfo;

    bool m_IsTextVisible;
    bool m_IsPlayerInRange;
    bool m_IsTextPositioned;
    
    float waitTime = 1.0f;
    float timer = 0.0f;

    void Start() {
        treeInfo = tree.GetComponent<TreeInfo>();
        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
    }

    void Update (){
        if (m_IsPlayerInRange && !m_IsTextVisible) {
            if(!m_IsTextPositioned){
                memoryPosition();
                m_IsTextPositioned = true;
            }
            timer += Time.deltaTime;
            if (timer < waitTime) {
                memory.GetComponent<TextMeshProUGUI>().alpha = timer;
            } else {
                m_IsTextVisible = true;
                timer = 0.0f;
            }
        } else if (!m_IsPlayerInRange && m_IsTextVisible) {
            timer += Time.deltaTime;
            if (timer < waitTime) {
                memory.GetComponent<TextMeshProUGUI>().alpha = (1 - timer);
            } else {
                m_IsTextVisible = false;
                m_IsTextPositioned = false;
                timer = 0.0f;
            }
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

    // set position of text in front of player at eye level
    void memoryPosition(){
        Vector3 playerDirection = player.forward;
        Quaternion playerRotation = player.rotation;
        float memoryDistance = tree.transform.localScale.x * -1f;

        Vector3 memoryPos = tree.transform.position + playerDirection * memoryDistance;

        memory.transform.position = memoryPos + new Vector3(0f, 1.7f, 0f);
        memory.transform.rotation = playerRotation;
    }
}
