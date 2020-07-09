using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMemoryUI : MonoBehaviour
{
    public GameObject memoryUI;
    public InputField inputField;

    bool m_MemoryUIVisible = false;

    // Update is called every frame
    void Update () {
        // TODO: if memory UI is visible, player should not be able to walk
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
                inputField.text = "";
                return;
            }
        }
    }

}
