﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public GameObject m_MainMenuUI;
    public GameObject m_NewGameUI;
    public GameObject m_ControlsUI;
    public GameObject m_GameOptionsUI;
    public GameObject m_SaveGameUI;
    public InputField m_SaveFileInputField;
    public GameObject m_LoadGameUI;
    public GameObject m_ExploreForestUI;
    public GameObject m_MemoryUI;
    public InputField m_MemoryInputField;
    public GameObject m_MemoryJournal;
    public GameObject m_InteractionUI;

    private GameObject m_SaveFileDropdown;
    private GameObject m_SaveFileErrorMessage;
    private GameObject m_SaveSuccessfulMessage;
    private GameObject m_LoadFileErrorMessage;
    private GameObject m_LoadFileDropdown;
    private GameObject m_ExploreForestDropdown;
    private string last;

    void Start(){
        m_LoadFileDropdown = m_LoadGameUI.transform.GetChild(4).gameObject;
        m_SaveFileDropdown = m_SaveGameUI.transform.GetChild(5).gameObject;
        m_SaveFileErrorMessage = m_SaveGameUI.transform.GetChild(6).gameObject;
        m_SaveSuccessfulMessage = m_SaveGameUI.transform.GetChild(7).gameObject;
        m_LoadFileErrorMessage = m_LoadGameUI.transform.GetChild(6).gameObject;
        m_ExploreForestDropdown = m_ExploreForestUI.transform.GetChild(4).gameObject;
        last = "main";
    }

    public void ResumeGame(){
        m_GameOptionsUI.SetActive(false);
    }

    public void ShowMainMenu() {
        last = "main";
        m_ExploreForestUI.SetActive(false);
        m_GameOptionsUI.SetActive(false);
        m_MainMenuUI.SetActive(true);
        m_MainMenuUI.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ShowControlsUI(){
        m_MainMenuUI.SetActive(false);
        m_GameOptionsUI.SetActive(false);
        m_ControlsUI.SetActive(true);
    }

    public void ShowGameOptionsUI(){
        last = "options";
        m_ControlsUI.SetActive(false);
        m_SaveGameUI.SetActive(false);
        m_LoadGameUI.SetActive(false);
        m_GameOptionsUI.SetActive(true);
    }

    public void ShowSaveGameUI(){
        m_SaveFileErrorMessage.SetActive(false);
        m_SaveSuccessfulMessage.SetActive(false);
        m_GameOptionsUI.SetActive(false);
        m_SaveGameUI.SetActive(true);

        // get all save files
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] saveFiles = dir.GetFiles("*.save*");

        var dropdown = m_SaveFileDropdown.GetComponent<Dropdown>();
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData("Select a file"));
        foreach (FileInfo file in saveFiles)
        {
            dropdown.options.Add(new Dropdown.OptionData(Path.GetFileNameWithoutExtension(file.ToString())));
        }

        StartCoroutine(DetermineSaveFileName());
    }

    public void BackFromControls(){
        m_ControlsUI.SetActive(false);
        if (last == "main") {
            m_MainMenuUI.SetActive(true);
        } else if(last == "options") {
            m_GameOptionsUI.SetActive(true);
        }
    }

    public void BackFromLoad(){
        m_LoadGameUI.SetActive(false);
        if (last == "main") {
            m_MainMenuUI.SetActive(true);
        } else if(last == "options") {
            m_GameOptionsUI.SetActive(true);
        }
    }

    public void ShowLoadGameUI(){
        m_LoadFileErrorMessage.SetActive(false);
        m_MainMenuUI.SetActive(false);
        m_GameOptionsUI.SetActive(false);
        m_LoadGameUI.GetComponent<CanvasGroup>().alpha = 1;
        m_LoadGameUI.SetActive(true);
        
        // get all save files
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] saveFiles = dir.GetFiles("*.save*");

        var dropdown = m_LoadFileDropdown.GetComponent<Dropdown>();
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData("Select a file"));
        foreach (FileInfo file in saveFiles)
        {
            dropdown.options.Add(new Dropdown.OptionData(Path.GetFileNameWithoutExtension(file.ToString())));
        }
    }

    public void ShowExploreForestUI(){
        m_MainMenuUI.SetActive(false);
        m_GameOptionsUI.SetActive(false);
        m_ExploreForestUI.GetComponent<CanvasGroup>().alpha = 1;
        m_ExploreForestUI.SetActive(true);
    }

    public void ShowMemoryInputUI(){
        m_MemoryUI.SetActive(true);
        m_MemoryInputField.ActivateInputField();
    }

    private IEnumerator DetermineSaveFileName(){
        var dropdown = m_SaveFileDropdown.GetComponent<Dropdown>();
        while(m_SaveGameUI.activeInHierarchy) {
            string dropdownSelection = dropdown.options[dropdown.value].text;
            if(dropdownSelection != "Select a file") {
                m_SaveFileInputField.text = dropdownSelection;
                m_SaveFileInputField.GetComponent<InputField>().interactable = false;
            } else {
                m_SaveFileInputField.transform.GetComponent<InputField>().interactable = true;
            }
            yield return null;
        }

        m_SaveFileInputField.text = "";
        dropdown.value = 0;
    }

}
