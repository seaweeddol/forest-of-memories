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
    public TreeInteraction treeInteraction;
    
    private GameObject CloneTree(GameObject treeType) {
        // get players current position + 5 on Z axis
        Vector3 treePosition = player.transform.position + new Vector3(0, 0, 5);

        return Instantiate(treeType, treePosition, Quaternion.identity);
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
            clone.transform.localScale = new Vector3(scaledScore, scaledScore, scaledScore);
        }

        // assign sentiment analysis and user input to tree
        TreeInteraction treeInteraction = clone.GetComponent<TreeInteraction>();
        treeInteraction.score = score;
        treeInteraction.tone = tone;
        treeInteraction.memory = memory;

        // make tree visible
        clone.SetActive(true);
    }
}
