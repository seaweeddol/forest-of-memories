﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> trees = new List<GameObject>();
    public GameObject spawnTree;
    public GameObject m_ControlsUI;
    public GameObject m_GameOptionsUI;

    private SpawnTree spawnTreeScript;

    void Start() {
        spawnTreeScript = spawnTree.GetComponent<SpawnTree>();
    }

    public void ShowControlsUI(){
        m_ControlsUI.SetActive(true);
        m_GameOptionsUI.SetActive(false);
    }

    public void ShowGameOptionsUI(){
        m_GameOptionsUI.SetActive(true);
        m_ControlsUI.SetActive(false);
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved" + file);
    }

    public void LoadGame()
    { 
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < save.treePositions.Count; i++)
            {
                spawnTreeScript.CreateTree(save.treePositions[i], save.treeRotations[i], save.treeScales[i], save.treeStrongestTones[i], save.treeScores[i], save.treeMemories[i], save.treeTones[i].allTones);
            }

            Debug.Log(save.treePositions.Count + " trees loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
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
            save.treeScales.Add(tree.transform.localScale);

            save.treeStrongestTones.Add(tree.GetComponent<TreeInfo>().strongestTone);
            save.treeScores.Add(tree.GetComponent<TreeInfo>().score);
            save.treeMemories.Add(tree.GetComponent<TreeInfo>().memory);

            Save.AllTonesContainer allTonesContainer = new Save.AllTonesContainer();
            allTonesContainer.allTones = tree.GetComponent<TreeInfo>().allTones;
            save.treeTones.Add(allTonesContainer);
        }

        return save;
    }
}