using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public static class TransformExtensions
{
    public static void ResetTransform(this Transform trans)
    {
        trans.SetTransformProperties(Vector3.zero, Quaternion.identity, Vector3.one);
    }

    public static void SetTransformProperties(this Transform trans, Transform properties)
    {
        trans.SetTransformProperties(properties.position, properties.rotation, properties.localScale);
    }

    public static void SetTransformProperties(this Transform trans, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        trans.position = position;
        trans.rotation = rotation;
        trans.localScale = scale;
    }
    
    public static void SetTransformProperties(this Transform trans, Quaternion rotation, Vector3 scale)
    {
        trans.rotation = rotation;
        trans.localScale = scale;
    }
    
    public static void SetTransformProperties(this Transform trans, Vector3 position, Vector3 scale)
    {
        trans.position = position;
        trans.localScale = scale;
    }
    
    public static void SetTransformProperties(this Transform trans, Vector3 position, Quaternion rotation)
    {
        trans.position = position;
        trans.rotation = rotation;
    }
    
}
