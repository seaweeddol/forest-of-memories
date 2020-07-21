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
    public GameObject m_MemoryUI;
    public InputField m_MemoryInputField;
    public GameObject m_MemoryJournal;
    public GameObject m_InteractionUI;

    private GameObject m_SaveFileDropdown;
    private GameObject m_LoadFileDropdown;

    void Start(){
        m_LoadFileDropdown = m_LoadGameUI.transform.GetChild(4).gameObject;
        m_SaveFileDropdown = m_SaveGameUI.transform.GetChild(5).gameObject;
    }

    public void ResumeGame(){
        m_ControlsUI.SetActive(false);
    }

    // TODO: determine which screen to go 'back' to from LoadGameUI. if player is in a game, go back to m_GameOptionsUI. If player is coming from main menu, go back to m_MainMenuUI

    public void ShowMainMenu() {
        DeactivateAll();
        m_MainMenuUI.SetActive(true);
        m_MainMenuUI.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ShowControlsUI(){
        DeactivateAll();
        m_ControlsUI.SetActive(true);
        // m_GameOptionsUI.SetActive(false);
        // m_SaveGameUI.SetActive(false);
    }

    public void ShowGameOptionsUI(){
        DeactivateAll();
        m_GameOptionsUI.SetActive(true);
        // m_ControlsUI.SetActive(false);
        // m_SaveGameUI.SetActive(false);
    }

    public void ShowSaveGameUI(){
        // m_GameOptionsUI.SetActive(false);
        // m_ControlsUI.SetActive(false);
        DeactivateAll();
        m_SaveGameUI.SetActive(true);

        // get all save files
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] saveFiles = dir.GetFiles("*.save*");

        var dropdown = m_SaveFileDropdown.GetComponent<Dropdown>();
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData("Select a file"));
        foreach (FileInfo file in saveFiles)
        {
            dropdown.options.Add(new Dropdown.OptionData(Path.GetFileName(file.ToString())));
        }
    }

    public void ShowLoadGameUI(){
        DeactivateAll();
        m_LoadGameUI.SetActive(true);
        
        // get all save files
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] saveFiles = dir.GetFiles("*.save*");

        var dropdown = m_LoadFileDropdown.GetComponent<Dropdown>();
        dropdown.options.Clear();
        dropdown.options.Add(new Dropdown.OptionData("Select a file"));
        foreach (FileInfo file in saveFiles)
        {
            dropdown.options.Add(new Dropdown.OptionData(Path.GetFileName(file.ToString())));
        }
    }

    public void ShowMemoryInputUI(){
        DeactivateAll();
        m_MemoryUI.SetActive(true);
        m_MemoryInputField.ActivateInputField();
    }

    public void DeactivateAll(){
        m_MainMenuUI.SetActive(false);
        m_NewGameUI.SetActive(false);
        m_ControlsUI.SetActive(false);
        m_GameOptionsUI.SetActive(false);
        m_SaveGameUI.SetActive(false);
        m_LoadGameUI.SetActive(false);
        m_MemoryUI.SetActive(false);
        m_MemoryJournal.SetActive(false);
        m_InteractionUI.SetActive(false);
        m_MemoryInputField.text = "";
    }
}