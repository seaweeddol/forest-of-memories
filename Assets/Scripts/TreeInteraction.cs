using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseOver(){
        Debug.Log("it's angry");
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode); 
    }

    void OnMouseExit(){
        Debug.Log("it's not angry");
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
