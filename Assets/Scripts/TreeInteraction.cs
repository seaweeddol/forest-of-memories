using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

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
        Debug.Log(memory);
    }
}
