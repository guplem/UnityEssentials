using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public static class TransformExtensions
{
    /// <summary>
    /// Sets the transform position, rotation and scale to the default values. Position = (0,0,0), Rotation = Quaternion.identity, Scale = (1,1,1).
    /// </summary>
    public static void ResetTransform(this Transform trans)
    {
        trans.SetProperties(Vector3.zero, Quaternion.identity, Vector3.one);
    }

    /// <summary>
    /// Sets the transform position, rotation and scale to the same values in the given transform.
    /// </summary>
    /// <param name="properties">The transform from which the position, rotation and scale will be copied.</param>
    public static void SetProperties(this Transform trans, Transform properties)
    {
        trans.SetProperties(properties.position, properties.rotation, properties.localScale);
    }
    /// <summary>
    /// Sets the transform position, rotation and scale to the given values.
    /// </summary>
    /// <param name="position">The position to be set in the transform.</param>
    /// <param name="rotation">The rotation to be set in the transform.</param>
    /// <param name="scale">The scale to be set in the transform.</param>
    public static void SetProperties(this Transform trans, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        trans.position = position;
        trans.rotation = rotation;
        trans.localScale = scale;
    }
    /// <summary>
    /// Sets the transform rotation and scale to the given values.
    /// </summary>
    /// <param name="rotation">The rotation to be set in the transform.</param>
    /// <param name="scale">The scale to be set in the transform.</param>
    public static void SetProperties(this Transform trans, Quaternion rotation, Vector3 scale)
    {
        trans.rotation = rotation;
        trans.localScale = scale;
    }
    /// <summary>
    /// Sets the transform position and scale to the given values.
    /// </summary>
    /// <param name="position">The position to be set in the transform.</param>
    /// <param name="scale">The scale to be set in the transform.</param>
    public static void SetProperties(this Transform trans, Vector3 position, Vector3 scale)
    {
        trans.position = position;
        trans.localScale = scale;
    }
    /// <summary>
    /// Sets the transform position and rotation to the given values.
    /// </summary>
    /// <param name="position">The position to be set in the transform.</param>
    /// <param name="rotation">The rotation to be set in the transform.</param>
    public static void SetProperties(this Transform trans, Vector3 position, Quaternion rotation)
    {
        trans.position = position;
        trans.rotation = rotation;
    }
    
    /// <summary>
    /// Find the rotation to look at a target.
    /// </summary>
    /// <param name="target">The thing to look at.</param>
    /// <returns>The needed rotation to look at the target.</returns>
    public static Quaternion GetLookAtRotation(this Transform self, Vector3 target)
    {
        return Quaternion.LookRotation(target - self.position);
    }
    /// <summary>
    /// Find the rotation to look at a target.
    /// </summary>
    /// <param name="target">The thing to look at.</param>
    /// <returns>The needed rotation to look at the target.</returns>
    public static Quaternion GetLookAtRotation(this Transform self, Transform target)
    {
        return GetLookAtRotation(self, target.position);
    }

    /// <summary>
    /// Instantly look in the opposite direction of the target.
    /// </summary>
    /// <param name="target">The thing to look away from</param>
    public static void LookAway(this Transform self, Vector3 target)
    {
        self.rotation = GetLookAwayRotation(self, target);
    }
    /// <summary>
    /// Instantly look in the opposite direction of the target.
    /// </summary>
    /// <param name="target">The thing to look away from.</param>
    public static void LookAway(this Transform self, Transform target)
    {
        self.rotation = GetLookAwayRotation(self, target);
    }
    
    /// <summary>
    /// Find the rotation to look in the opposite direction of the target.
    /// </summary>
    /// <param name="target">The thing to look away from.</param>
    /// <returns>The needed rotation to look away from the target.</returns>
    public static Quaternion GetLookAwayRotation(this Transform self, Vector3 target)
    {
        return Quaternion.LookRotation(self.position - target);
    }

    /// <summary>
    /// Find the rotation to look away from a target transform.
    /// </summary>
    /// <param name="target">The thing to look away from.</param>
    /// <returns>The needed rotation to look away from the target.</returns>
    public static Quaternion GetLookAwayRotation(this Transform self, Transform target)
    {
        return GetLookAwayRotation(self, target.position);
    }
}
