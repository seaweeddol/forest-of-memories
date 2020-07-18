using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // If the value is array of objects, you should prepare the class for the object. One object, one class!!

    // things to save
    // tree object
    // type of tree
    // size of tree (or just score?)
    // tree info
    // tree position
    // tree rotation
        // can I just save the entire object?

    [System.Serializable]
    public class Tree{
        public string potion_name;
        public int value;
        public List<Effect> effect = new List<Effect>();
    }

    [System.Serializable]
    public class Effect{
        public string name;
        public string desc;
    }

}
