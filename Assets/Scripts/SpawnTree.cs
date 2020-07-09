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
    

    private GameObject CloneTree(GameObject treeType) {
        Vector3 treePosition = player.transform.position;

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
        // if neutral, scale will be inherited from parent
        if(clone.tag != "neutral") {
            float scaledScore = (float)score * 3;
            clone.transform.localScale = new Vector3(scaledScore, scaledScore, scaledScore);
        }

        // TODO: spawn tree at current mouse location
        // or - spawn tree at current player location + ~couple feet in front of them

        clone.SetActive(true);
    }
}
