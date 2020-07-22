using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        GameObject button = pointerEventData.pointerEnter;

        button.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        GameObject button = pointerEventData.pointerEnter;

        button.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}