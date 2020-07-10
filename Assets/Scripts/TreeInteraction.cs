using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeInteraction : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GameObject memoryTextContainer;
    public GameObject memoryText;

    public double score;
    public string tone;
    public string memory;

    void OnMouseOver(){
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); 
    }

    void OnMouseExit(){
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    void OnMouseDown(){
        // TODO: show text in front of tree
        // hide text on click or after a certain distance
        TextMeshProUGUI textMeshPro = memoryText.GetComponent<TextMeshProUGUI>();
        textMeshPro.SetText(memory);
        memoryTextContainer.SetActive(true);
    }
}
