using UnityEngine;
using System;
using System.Collections;

// https://answers.unity.com/questions/956047/serialize-quaternion-or-vector3.html
// Since unity doesn't flag the Quaternion as serializable, we
// need to create our own version
[System.Serializable]
public struct SerializableQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;
    
    // Constructor
    public SerializableQuaternion(float rX, float rY, float rZ, float rW)
    {
        x = rX;
        y = rY;
        z = rZ;
        w = rW;
    }
    
    // Returns a string representation of the object
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }
    
    // Automatic conversion from SerializableQuaternion to Quaternion
    public static implicit operator Quaternion(SerializableQuaternion rValue)
    {
        return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }
    
    // Automatic conversion from Quaternion to SerializableQuaternion
    public static implicit operator SerializableQuaternion(Quaternion rValue)
    {
        return new SerializableQuaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }
}