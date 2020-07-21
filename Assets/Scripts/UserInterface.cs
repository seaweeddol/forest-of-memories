using System.IO;
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

    public void ShowMainMenu() {
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
        // TODO: maybe split this up into multiple functions
        m_ControlsUI.SetActive(false);
        m_SaveGameUI.SetActive(false);
        m_LoadGameUI.SetActive(false);
        m_GameOptionsUI.SetActive(true);
    }

    public void ShowSaveGameUI(){
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
            dropdown.options.Add(new Dropdown.OptionData(Path.GetFileName(file.ToString())));
        }
    }

    public void BackToMainFromLoad(){
        m_LoadGameUI.SetActive(false);
        m_MainMenuUI.SetActive(true);
    }

    public void ShowLoadGameUI(){
        m_MainMenuUI.SetActive(false);
        m_GameOptionsUI.SetActive(false);
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
        m_MemoryUI.SetActive(true);
        m_MemoryInputField.ActivateInputField();
    }
}
