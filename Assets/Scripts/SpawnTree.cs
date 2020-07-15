using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public GameObject player;
    public Transform terrain;
    public GameObject ParentTree;
    // TODO: make trees private, assign using child of ParentTree
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
        // get player coords
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 10;

        // set tree spawn position to player position + spawn distance
        Vector3 spawnPos = playerPos + playerDirection*spawnDistance; 
        spawnPos += new Vector3(0, 10, 0); // move spawnPos up in case of upward slope
        GameObject newTree = Instantiate(treeType, spawnPos, playerRotation);

        // save initial scale & then set to 0
        Vector3 currentScale = newTree.transform.localScale;
        newTree.transform.localScale = new Vector3(0, 0, 0);

        RaycastHit hit;
        // create Ray that shoots down from the tree's position
        var ray = new Ray(newTree.transform.position, Vector3.down);
        
        // make tree spawn attached to terrain on slopes
        foreach(Transform child in terrain) {
            // if ray hits something, set position and rotation to that hit point
            if (child.GetComponent<Collider>().Raycast(ray, out hit, 1000)) {
                Vector3 hitPoint = hit.point;
                newTree.transform.rotation = Quaternion.FromToRotation(newTree.transform.up, hit.normal)*newTree.transform.rotation; // adjust for slopes
                newTree.transform.position = hitPoint;
            }
        }

        StartCoroutine(GrowTree(newTree, currentScale, 1f));

        return newTree;
    }

    // make tree grow over set amount of time
    private IEnumerator GrowTree(GameObject tree, Vector3 currentScale, double score){
        yield return new WaitForSeconds(.5f); // wait .5 sec before starting
        Vector3 newScale = currentScale;

        // set max scale to grow to (neutral trees will keep original scale)
        if(tree.tag != "neutral") {
            float scaledScore = (float)score * 2.5f;
            newScale = new Vector3(currentScale.x * scaledScore, currentScale.y * scaledScore, currentScale.z * scaledScore);
        }

        // while time passed is less than 1sec, update tree scale
        float ratio = 0f;
        while (ratio/1f < 1f) {
            tree.transform.localScale = new Vector3((newScale.x * ratio), (newScale.y * ratio), (newScale.z * ratio));
            ratio += Time.deltaTime;
            yield return null;
        }

        // set tree scale to final scale (since loop will stop at a little less than 1)
        tree.transform.localScale = newScale;
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

        Transform tree = clone.transform.GetChild(0);

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
