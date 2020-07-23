﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryTextPosition : MonoBehaviour
{
    public GameObject memoryContainer;
    public GameObject tree;
    public Transform rotator;
    public float degreesPerSecond;
    public Material treeMaterial;
    
    private Transform player;
    private TreeInfo treeInfo;
    private Transform memory;

    private bool m_IsTextVisible;
    private bool m_IsPlayerInRange;
    private bool m_IsTextPositioned;
    
    void Start() {
        treeInfo = tree.GetComponent<TreeInfo>();
        memory = memoryContainer.transform.GetChild(0);
        memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
        player = GameObject.Find("Player").transform;
    }

    void OnTriggerEnter (Collider other) {
        if(other.transform == player) {
            m_IsPlayerInRange = true;
            // StartCoroutine(FadeInText());
            StartCoroutine(LookAtPlayer());
            player.GetComponent<PlayerController>().treesInRange.Add(tree);
        }
    }

    void OnTriggerExit (Collider other) {
        if(other.transform == player) {
            m_IsPlayerInRange = false;
            // StartCoroutine(FadeOutText());
            player.GetComponent<PlayerController>().treesInRange.Remove(tree);
            tree.GetComponent<Renderer>().material = treeMaterial;

        }
    }

    // set position of text in front of player at eye level
    void memoryPosition(){
        Vector3 playerDirection = player.forward;
        Quaternion playerRotation = player.rotation;
        float memoryDistance = tree.transform.localScale.x * -1.25f;

        Vector3 memoryPos = tree.transform.position + playerDirection * memoryDistance;

        memoryContainer.transform.position = memoryPos + new Vector3(0f, 1.8f, 0f);
        memoryContainer.transform.rotation = playerRotation;
    }

    private IEnumerator LookAtPlayer(){
        StartCoroutine(FadeInText());
        while(m_IsPlayerInRange) {
            Vector3 dirFromMeToTarget = player.position - rotator.position;
            dirFromMeToTarget.y = 0.0f;

            Quaternion lookRotation = Quaternion.LookRotation(dirFromMeToTarget);

            rotator.rotation = Quaternion.Lerp(rotator.rotation, lookRotation, Time.deltaTime * (degreesPerSecond/360.0f));
            yield return null;
        }

        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeInText(){
        // memoryPosition();

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
