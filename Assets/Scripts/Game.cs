using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject[] trees;

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    // public void LoadGame()
    // { 
    //     // 1
    //     if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
    //     {
    //         ClearBullets();
    //         ClearRobots();
    //         RefreshRobots();

    //         // 2
    //         BinaryFormatter bf = new BinaryFormatter();
    //         FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
    //         Save save = (Save)bf.Deserialize(file);
    //         file.Close();

    //         // 3
    //         for (int i = 0; i < save.livingTargetPositions.Count; i++)
    //         {
    //         int position = save.livingTargetPositions[i];
    //         Target target = targets[position].GetComponent<Target>();
    //         target.ActivateRobot((RobotTypes)save.livingTargetsTypes[i]);
    //         target.GetComponent<Target>().ResetDeathTimer();
    //         }

    //         // 4
    //         shotsText.text = "Shots: " + save.shots;
    //         hitsText.text = "Hits: " + save.hits;
    //         shots = save.shots;
    //         hits = save.hits;

    //         Debug.Log("Game Loaded");

    //         Unpause();
    //     }
    //     else
    //     {
    //         Debug.Log("No game saved!");
    //     }
    // }

    public void SaveAsJSON()
    {
        Save save = CreateSaveGameObject();
        string json = JsonUtility.ToJson(save);

        Debug.Log("Saving as JSON: " + json);
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        // int i = 0; seems unnecessary but will keep for now
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
            save.treeTones.Add(tree.GetComponent<TreeInfo>().allTones);
            // i++; why is this here? doesn't seem to be doing anything
        }

        return save;
    }
}
