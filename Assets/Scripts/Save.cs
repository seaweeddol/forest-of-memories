using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<SerializableVector3> treePositions = new List<SerializableVector3>();
    public List<SerializableQuaternion> treeRotations = new List<SerializableQuaternion>();
    public List<SerializableVector3> treeScales = new List<SerializableVector3>();

    public List<string> treeStrongestTones = new List<string>();

    public List<double> treeScores = new List<double>();

    public List<string> treeMemories = new List<string>();

    public List<System.DateTime> treeTimeStamps = new List<System.DateTime>();

    public List<AllTonesContainer> treeTones = new List<AllTonesContainer>();

    [System.Serializable]
    public class AllTonesContainer
    {
        public List<string> allTones;
    }
}
