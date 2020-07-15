using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public GameObject player;
    public GameObject ParentTree;
    public GameObject angerTree;
    public GameObject joyTree;
    public GameObject sadnessTree;
    public GameObject fearfulTree;
    public GameObject analyticalTree;
    public GameObject confidentTree;
    public GameObject tentativeTree;
    public GameObject neutralTree;
    
    // clone tree in front of current player position
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

        // determine type of tree based on tone
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
        clone.transform.SetParent(ParentTree.transform);

        // set position of children to parent position
        int children = clone.transform.childCount;
        for (int i = 0; i < children; ++i) {
            clone.transform.GetChild(i).transform.position = clone.transform.position;
        }

        // scale tree depending on score (neutral tree scale will be inherited from parent since score is 0)
        Transform tree = clone.transform.GetChild(0);
        if(clone.tag != "neutral") {
            float scaledScore = (float)score * 2.5f;
            Vector3 currentScale = clone.transform.localScale;
            tree.transform.localScale = new Vector3(currentScale.x * scaledScore, currentScale.y * scaledScore, currentScale.z * scaledScore);
        }

        // update collider radius based on tree scale
        Transform playerInRange = clone.transform.GetChild(2);
        playerInRange.GetComponent<CapsuleCollider>().radius += tree.transform.localScale.x;

        // assign sentiment analysis and user input to tree
        TreeInfo treeInfo = tree.transform.GetComponent<TreeInfo>();
        treeInfo.score = score;
        treeInfo.sentiment = tone;
        treeInfo.memory = memory;

        // make tree visible
        clone.SetActive(true);
    }
}
