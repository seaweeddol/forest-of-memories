using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryTextPosition : MonoBehaviour
{
    public Transform player;
    // public GameObject memoryContainer;
    public GameObject memory;
    public Collider collider;
    public TreeInfo treeInfo;
    bool m_IsTextVisible = false;
    bool m_IsPlayerInRange;
    // public float alpha;
    float waitTime = 2.0f;
    float timer = 0.0f;

    void Start() {
        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
    }

    void Update (){
        // TODO: make text move with camera aim
        if (m_IsPlayerInRange && !m_IsTextVisible) {
            timer += Time.deltaTime;
            if (timer < waitTime) {
                memory.GetComponent<TextMeshProUGUI>().alpha = timer / 2;
                Debug.Log("current alpha: " + memory.GetComponent<TextMeshProUGUI>().alpha);
            } else {
                m_IsTextVisible = true;
                timer = 0.0f;
            }
        } else if (!m_IsPlayerInRange && m_IsTextVisible) {
            timer += Time.deltaTime;
            if (timer < waitTime) {
                memory.GetComponent<TextMeshProUGUI>().alpha -= timer / 2;
                Debug.Log("current alpha: " + memory.GetComponent<TextMeshProUGUI>().alpha);
            }
        }
        // show memory if user is within range of tree
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

    void FadeOutText(){
        // fade from opaque to transparent
        // loop over 1 second backwards
        
        Debug.Log("fade out");
        for (float i = 5; i >= 0; i -= Time.deltaTime) {
            // set color with i as alpha
            Debug.Log(i);
            memory.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i);
        }
        m_IsTextVisible = false;
    }

    void FadeInText(){
        // fade from transparent to opaque
        // loop over 1 second
        float waitTime = 10.0f;
        float timer = 0.0f;

        while (timer < waitTime)
        {
            timer += Time.deltaTime;
            Debug.Log("current time: " + timer);

            memory.GetComponent<TextMeshProUGUI>().alpha = timer;
        }

        // for (float i = 0; i <= 5; i += Time.deltaTime){
        //     // set color with i as alpha
        //     Debug.Log(i);
        //     memory.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i);
        // }
        m_IsTextVisible = true;
    }
}
