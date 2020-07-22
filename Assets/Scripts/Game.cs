﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> trees = new List<GameObject>();
    public GameObject spawnTree;
    public GameObject m_MainMenuUI;
    public GameObject m_NewGameUI;
    public GameObject m_MemoryUI;
    public InputField m_MemoryInputField;
    public InputField m_SaveFileInputField;
    public GameObject m_SaveFileDropdown;
    public GameObject m_SaveFileErrorMessage;
    public GameObject m_LoadFileDropdown;
    public GameObject m_LoadFileErrorMessage;

    private SpawnTree spawnTreeScript;
    private int savedGames = 0;

    void Start() {
        spawnTreeScript = spawnTree.GetComponent<SpawnTree>();
    }

    public void ResetGame() {
        trees = new List<GameObject>();
        SceneManager.LoadScene("MainScene");
    }

    public void NewGame(){
        StartCoroutine(FadeOutMainMenu());
        StartCoroutine(FadeInNewGameUI());
        StartCoroutine(WaitForSpace());
    }

    private IEnumerator FadeOutMainMenu(){
        // while time passed is less than 1sec, update menu alpha
        float ratio = 0f;
        while (ratio/1f < 1f) {
            m_MainMenuUI.GetComponent<CanvasGroup>().alpha = (1 - ratio);
            ratio += Time.deltaTime;
            yield return null;
        }

        m_MainMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        m_MainMenuUI.SetActive(false);
    }

    private IEnumerator FadeInNewGameUI(){
        // while time passed is less than 1sec, update menu alpha
        float ratio = 0f;
        while (ratio/1f < 1f) {
            m_NewGameUI.GetComponent<CanvasGroup>().alpha = ratio;
            ratio += Time.deltaTime;
            yield return null;
        }

        m_NewGameUI.GetComponent<CanvasGroup>().alpha = 1;
    }

    private IEnumerator WaitForSpace(){
        while(!Input.GetKeyDown("space")) {
            yield return null;
        }

        m_NewGameUI.SetActive(false);
        m_NewGameUI.GetComponent<CanvasGroup>().alpha = 0;

        m_MemoryUI.SetActive(true);
        m_MemoryInputField.ActivateInputField();
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();
        FileStream file;

        BinaryFormatter bf = new BinaryFormatter();

        var dropdown = m_SaveFileDropdown.GetComponent<Dropdown>();
        string fileName = dropdown.options[dropdown.value].text;

        if(fileName == "Select a file" && m_SaveFileInputField.text == "") {
            m_SaveFileErrorMessage.SetActive(true);
        } else {
            if (m_SaveFileInputField.text != "") {
                savedGames += 1;
                fileName = m_SaveFileInputField.text;
                fileName = Regex.Replace(fileName, @"[^a-zA-Z0-9 ]", "");
            }

            file = File.Create(Application.persistentDataPath + "/" + fileName + ".save");
            
            bf.Serialize(file, save);
            file.Close();

            Debug.Log("Game Saved" + file);
            m_SaveFileErrorMessage.SetActive(false);
        }

        // TODO: change the company name in player settings
    }

    public void LoadGame()
    { 
        // TODO: start player at position they were at (will need to save player position)
        var dropdown = m_LoadFileDropdown.GetComponent<Dropdown>();
        string fileName = dropdown.options[dropdown.value].text;
        string filePath = Application.persistentDataPath + "/" + fileName + ".save";

        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < save.treePositions.Count; i++)
            {
                spawnTreeScript.CreateTree(save.treePositions[i], save.treeRotations[i], save.treeScales[i], save.treeStrongestTones[i], save.treeScores[i], save.treeMemories[i], save.treeTones[i].allTones, save.treeTimeStamps[i]);
            }

            StartCoroutine(FadeOutMainMenu());
            m_NewGameUI.SetActive(false);

            Debug.Log(save.treePositions.Count + " trees loaded");
        }
        else
        {
            m_LoadFileErrorMessage.SetActive(true);
        }
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);

        Debug.Log(Application.persistentDataPath);
        Debug.Log("Saving as JSON: " + json);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        foreach (GameObject treeGameObject in trees)
        {
            // get the tree
            GameObject tree = treeGameObject.transform.GetChild(0).gameObject;

            // save all the data
            save.treePositions.Add(tree.transform.position);
            save.treeRotations.Add(tree.transform.rotation);
            save.treeScales.Add(tree.transform.lossyScale);

            save.treeStrongestTones.Add(tree.GetComponent<TreeInfo>().strongestTone);
            save.treeScores.Add(tree.GetComponent<TreeInfo>().score);
            save.treeMemories.Add(tree.GetComponent<TreeInfo>().memory);
            save.treeTimeStamps.Add(tree.GetComponent<TreeInfo>().dateTime);

            Save.AllTonesContainer allTonesContainer = new Save.AllTonesContainer();
            allTonesContainer.allTones = tree.GetComponent<TreeInfo>().allTones;
            save.treeTones.Add(allTonesContainer);
        }

        return save;
    }
}
