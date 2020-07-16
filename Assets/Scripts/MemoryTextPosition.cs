using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryTextPosition : MonoBehaviour
{
    public GameObject memoryContainer;
    public GameObject tree;
    
    private Transform player;
    private TreeInfo treeInfo;
    private Transform memory;

    bool m_IsTextVisible;
    bool m_IsPlayerInRange;
    bool m_IsTextPositioned;
    
    void Start() {
        treeInfo = tree.GetComponent<TreeInfo>();
        memory = memoryContainer.transform.GetChild(0);
        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
        player = GameObject.Find("Player").transform;
    }

    void Update (){
        // if (m_IsPlayerInRange && !m_IsTextVisible) {
        //     if(!m_IsTextPositioned){
        //         memoryPosition();
        //         m_IsTextPositioned = true;
        //     }
        //     timer += Time.deltaTime;
        //     if (timer < waitTime) {
        //         memory.GetComponent<TextMeshProUGUI>().alpha = timer;
        //     } else {
        //         m_IsTextVisible = true;
        //         timer = 0.0f;
        //     }
        // } else if (!m_IsPlayerInRange && m_IsTextVisible) {
        //     timer += Time.deltaTime;
        //     if (timer < waitTime) {
        //         memory.GetComponent<TextMeshProUGUI>().alpha = (1 - timer);
        //     } else {
        //         m_IsTextVisible = false;
        //         m_IsTextPositioned = false;
        //         timer = 0.0f;
        //     }
        // }
    }

    void OnTriggerEnter (Collider other) {
        if(other.transform == player) {
            StartCoroutine(FadeInText());
        }
    }

    void OnTriggerExit (Collider other) {
        if(other.transform == player) {
            StartCoroutine(FadeOutText());
        }
    }

    // set position of text in front of player at eye level
    void memoryPosition(){
        Vector3 playerDirection = player.forward;
        Quaternion playerRotation = player.rotation;
        float memoryDistance = tree.transform.localScale.x * -1f;

        Vector3 memoryPos = tree.transform.position + playerDirection * memoryDistance;

        memoryContainer.transform.position = memoryPos + new Vector3(0f, 1.8f, 0f);
        memoryContainer.transform.rotation = playerRotation;
    }

    private IEnumerator FadeInText(){
        memoryPosition();

        // while time passed is less than 1sec, update text alpha
        float ratio = 0f;
        while (ratio/1f < 1f) {
            memory.GetComponent<TextMeshProUGUI>().alpha = ratio;
            ratio += Time.deltaTime;
            yield return null;
        }

        memory.GetComponent<TextMeshProUGUI>().alpha = 1;
    }

    private IEnumerator FadeOutText(){
        // while time passed is less than 1sec, update text alpha
        float ratio = 0f;
        while (ratio/1f < 1f) {
            memory.GetComponent<TextMeshProUGUI>().alpha = (1 - ratio);
            ratio += Time.deltaTime;
            yield return null;
        }

        memory.GetComponent<TextMeshProUGUI>().alpha = 0;
    }
}
