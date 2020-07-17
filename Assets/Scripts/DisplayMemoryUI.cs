using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMemoryUI : MonoBehaviour
{
    public GameObject memoryUI;
    public GameObject controlsUI;
    public InputField inputField;

    // Update is called every frame
    void Update () {
        // if(Input.GetKeyDown("space")) {
        //     memoryUI.SetActive(true);
        //     inputField.ActivateInputField();
        // }

        // if(Input.GetKeyDown("escape")) {
        //     if(controlsUI.activeInHierarchy || memoryUI.activeInHierarchy){
        //         controlsUI.SetActive(false);
        //         memoryUI.SetActive(false);
        //         inputField.text = "";
        //     } else {
        //         controlsUI.SetActive(true);
        //     }
        // }
    }
}
