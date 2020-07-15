using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeInteraction : MonoBehaviour
{
    // public Texture2D cursorTexture;
    // public CursorMode cursorMode = CursorMode.Auto;
    // public Vector2 hotSpot = Vector2.zero;
    // public GameObject memoryTextContainer;
    public GameObject memoryText;
    public GameObject tree;
    TreeInfo treeInfo;

    public GameObject fullMemoryContainer;
    public GameObject entryNumber;
    public GameObject date;
    public GameObject sentiment;
    public GameObject memory;

    // public Transform player;
    public GameObject cursor;

    void Start(){
        treeInfo = tree.GetComponent<TreeInfo>();
    }

    // TODO: instead of mouseover, do a range? show icon or text when in range "Press E to view this memory"

    void OnMouseOver(){
        // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        if (memoryText.GetComponent<TextMeshProUGUI>().alpha > 0.1){
            cursor.SetActive(true);
        }
    }

    void OnMouseExit(){
        // Cursor.SetCursor(null, Vector2.zero, cursorMode);
        cursor.SetActive(false);
    }

    void OnMouseDown(){
        // hide text on click or after a certain distance
        if(!fullMemoryContainer.activeInHierarchy) {
            // TODO: player cannot move or look around when memory journal is open
            fullMemoryContainer.SetActive(true);
            entryNumber.GetComponent<TextMeshProUGUI>().text = "Entry #1";
            date.GetComponent<TextMeshProUGUI>().text = "Date: " + treeInfo.dateTime;
            sentiment.GetComponent<TextMeshProUGUI>().text = "Sentiment: " + treeInfo.sentiment;
            memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
        } else {
            // TODO: instead of click, use escape to exit memory
            fullMemoryContainer.SetActive(false);
        }
    }
}
