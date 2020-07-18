using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<Vector3> treePositions = new List<Vector3>();
    public List<Quaternion> treeRotations = new List<Quaternion>();
    public List<Vector3> treeScales = new List<Vector3>();

    public List<string> treeStrongestTones = new List<string>();

    public List<double> treeScores = new List<double>();

    public List<string> treeMemories = new List<string>();

    public List<List<string>> treeTones = new List<List<string>>();

    // entry number can be found via loop iteration + 1?
    // things to save
    // tree object
    // tone
    // size of tree (or just score?)
    // tree info
    // tree position
    // tree rotation
        // can I just save the entire object?

    // all trees
        // single tree object
            // tree info
                // memory
                // score
                // tone
                // all tones

}
