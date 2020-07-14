using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMemoryUI : MonoBehaviour
{
    public GameObject memoryUI;
    public InputField inputField;

    // Update is called every frame
    void Update () {
        if(Input.GetKeyDown("space")) {
            inputField.Select();
            memoryUI.SetActive(true);
        }

        if(Input.GetKeyDown("escape")) {
            memoryUI.SetActive(false);
            inputField.text = "";
        }
    }

}
