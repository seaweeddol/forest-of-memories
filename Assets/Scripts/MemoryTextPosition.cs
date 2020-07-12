using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryTextPosition : MonoBehaviour
{
    public Transform player;
    public GameObject memory;
    public Collider collider;
    public TreeInfo treeInfo;

    bool m_IsTextVisible;
    bool m_IsPlayerInRange;
    
    float waitTime = 1.0f;
    float timer = 0.0f;

    void Start() {
        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
    }

    void Update (){
        // TODO: make text move with camera aim
        if (m_IsPlayerInRange && !m_IsTextVisible) {
            timer += Time.deltaTime;
            if (timer < waitTime) {
                memory.GetComponent<TextMeshProUGUI>().alpha = timer;
                Debug.Log("current alpha: " + memory.GetComponent<TextMeshProUGUI>().alpha);
            } else {
                m_IsTextVisible = true;
                timer = 0.0f;
            }
        } else if (!m_IsPlayerInRange && m_IsTextVisible) {
            timer += Time.deltaTime;
            if (timer < waitTime) {
                memory.GetComponent<TextMeshProUGUI>().alpha = (1 - timer);
                Debug.Log("current alpha: " + memory.GetComponent<TextMeshProUGUI>().alpha);
            } else {
                m_IsTextVisible = false;
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
            // FadeOutText();
        }
    }
}
