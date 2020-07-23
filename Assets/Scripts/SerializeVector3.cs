using UnityEngine;
using System;
using System.Collections;

// https://answers.unity.com/questions/956047/serialize-quaternion-or-vector3.html
// Since unity doesn't flag the Vector3 as serializable, we
// need to create our own version
[System.Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;
    
    // Constructor
    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }
    
    // Returns a string representation of the object
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }
    
    // Automatic conversion from SerializableVector3 to Vector3
    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }
    
    // Automatic conversion from Vector3 to SerializableVector3
    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}
