using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayMemoryUI : MonoBehaviour
{
    public GameObject memoryUI;

    bool m_MemoryUIVisible = false;

    // Update is called every frame
    void Update () {
        if(Input.GetKeyDown("space")) {
            if (!m_MemoryUIVisible) {
                m_MemoryUIVisible = true;
                memoryUI.SetActive(true);
                return;
            }
        }

        if(Input.GetKeyDown("escape")) {
            if (m_MemoryUIVisible) {
                m_MemoryUIVisible = false;
                memoryUI.SetActive(false);
                return;
            }
        }
    }

}
