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

    public void CreateTree(string tone) {
        GameObject clone;

        switch (tone) {
            case "Sadness":
                clone = Instantiate(sadnessTree);
                break;
            case "Anger":
                clone = Instantiate(angerTree);
                break;
            case "Joy":
                clone = Instantiate(joyTree);
                break;
            case "Fear":
                clone = Instantiate(fearfulTree);
                break;
            case "Analytical":
                clone = Instantiate(analyticalTree);
                break;
            case "Confident":
                clone = Instantiate(confidentTree);
                break;
            case "Tentative":
                clone = Instantiate(tentativeTree);
                break;
            default:
                clone = Instantiate(neutralTree);
                break;
        }

        clone.transform.SetParent(tree.transform);
        clone.SetActive(true);
    }
}
