using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeInteraction : MonoBehaviour
{
    public GameObject memoryText;
    public GameObject tree;
    public GameObject fullMemoryContainer;
    public GameObject cursor;
    TreeInfo treeInfo;

    private GameObject fullMemoryBook;
    private Transform entryNumber;
    private Transform date;
    private Transform sentiment;
    private Transform memory;

    // public Transform player;

    void Start(){
        treeInfo = tree.GetComponent<TreeInfo>();
        fullMemoryBook = fullMemoryContainer.transform.GetChild(0).gameObject;
        entryNumber = fullMemoryBook.transform.Find("EntryNumber");
        date = fullMemoryBook.transform.Find("Date");
        sentiment = fullMemoryBook.transform.Find("Sentiment");
        memory = fullMemoryBook.transform.Find("Panel/Memory");
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
        if(!fullMemoryBook.activeInHierarchy) {
            // TODO: player cannot move or look around when memory journal is open
            fullMemoryBook.SetActive(true);
            entryNumber.GetComponent<TextMeshProUGUI>().text = "Entry #1";
            date.GetComponent<TextMeshProUGUI>().text = "Date: " + treeInfo.dateTime;
            sentiment.GetComponent<TextMeshProUGUI>().text = "Sentiment: " + treeInfo.sentiment;
            memory.GetComponent<TextMeshProUGUI>().text = treeInfo.memory;
        } else {
            // TODO: instead of click, use escape to exit memory
            fullMemoryBook.SetActive(false);
        }
    }
}
