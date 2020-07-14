using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public GameObject tree;
    public GameObject angerTree;
    public GameObject joyTree;
    public GameObject sadnessTree;
    public GameObject fearfulTree;
    public GameObject analyticalTree;
    public GameObject confidentTree;
    public GameObject tentativeTree;
    public GameObject neutralTree;
    public GameObject player;
    // public TreeInteraction treeInteraction;
    public TreeInfo treeInfo;
    
    private GameObject CloneTree(GameObject treeType) {
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 10;

        Vector3 spawnPos = playerPos + playerDirection*spawnDistance;        
        return Instantiate(treeType, spawnPos, playerRotation);
    }

    public void CreateTree(string tone, double score, string memory) {
        GameObject clone;

        switch (tone) {
            case "Sadness":
                clone = CloneTree(sadnessTree);
                break;
            case "Anger":
                clone = CloneTree(angerTree);
                break;
            case "Joy":
                clone = CloneTree(joyTree);
                break;
            case "Fear":
                clone = CloneTree(fearfulTree);
                break;
            case "Analytical":
                clone = CloneTree(analyticalTree);
                break;
            case "Confident":
                clone = CloneTree(confidentTree);
                break;
            case "Tentative":
                clone = CloneTree(tentativeTree);
                break;
            default:
                clone = CloneTree(neutralTree);
                break;
        }

        // make clone a child of Trees
        clone.transform.SetParent(tree.transform);

        // scale tree depending on score
        // if neutral, scale will be inherited from parent since score is 0
        if(clone.tag != "neutral") {
            float scaledScore = (float)score * 3;
            Vector3 currentScale = clone.transform.localScale;
            clone.transform.localScale = new Vector3(currentScale.x * scaledScore, currentScale.y * scaledScore, currentScale.z * scaledScore);
        }

        // assign sentiment analysis and user input to tree
        TreeInfo treeInfo = clone.GetComponent<TreeInfo>();
        treeInfo.score = score;
        treeInfo.sentiment = tone;
        treeInfo.memory = memory;

        // make tree visible
        clone.SetActive(true);
    }
}
