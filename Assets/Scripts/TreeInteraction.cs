using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreeInteraction : MonoBehaviour
{
    // public Texture2D cursorTexture;
    // public CursorMode cursorMode = CursorMode.Auto;
    // public Vector2 hotSpot = Vector2.zero;
    public GameObject memoryTextContainer;
    public GameObject memoryText;
    public Transform player;

    public GameObject cursor;

    void OnMouseOver(){
        // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); 
        cursor.SetActive(true);
    }

    void OnMouseExit(){
        // Cursor.SetCursor(null, Vector2.zero, cursorMode);
        cursor.SetActive(false);
    }

    void OnMouseDown(){
        // TODO: show text in front of tree
        // hide text on click or after a certain distance
        // TextMeshProUGUI textMeshPro = memoryText.GetComponent<TextMeshProUGUI>();
        // textMeshPro.SetText(treeInfo.memory);
        // memoryTextContainer.SetActive(true);

        // set position of text in front of player at eye level
        Vector3 playerPos = player.position;
        Vector3 playerDirection = player.forward;
        Quaternion playerRotation = player.rotation;
        float memoryDistance = 1.5f;

        Vector3 memoryPos = playerPos + playerDirection * memoryDistance;

        memoryText.transform.position = memoryPos;
        memoryText.transform.position += new Vector3(0f, 1.5f, 0f);
        memoryText.transform.rotation = playerRotation;

        memoryText.GetComponent<TextMeshProUGUI>().alpha = 1;

    }
}
