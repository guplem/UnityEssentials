using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
    /// <summary>
    /// Extensions for the Transform component
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Sets the transform position, rotation and scale to the default values. Position = (0,0,0), Rotation = Quaternion.identity, Scale = (1,1,1).
        /// </summary>
        public static void ResetTransform(this Transform self)
        {
            self.SetProperties(Vector3.zero, Quaternion.identity, Vector3.one);
        }

        /// <summary>
        /// Sets the transform position, rotation and scale to the same values in the given transform.
        /// </summary>
        /// <param name="properties">The transform from which the position, rotation and scale will be copied.</param>
        public static void SetProperties(this Transform self, Transform properties)
        {
            self.SetProperties(properties.position, properties.rotation, properties.localScale);
        }
        /// <summary>
        /// Sets the transform position, rotation and scale to the given values.
        /// </summary>
        /// <param name="position">The position to be set in the transform.</param>
        /// <param name="rotation">The rotation to be set in the transform.</param>
        /// <param name="scale">The scale to be set in the transform.</param>
        public static void SetProperties(this Transform self, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            self.position = position;
            self.rotation = rotation;
            self.localScale = scale;
        }
        /// <summary>
        /// Sets the transform rotation and scale to the given values.
        /// </summary>
        /// <param name="rotation">The rotation to be set in the transform.</param>
        /// <param name="scale">The scale to be set in the transform.</param>
        public static void SetProperties(this Transform self, Quaternion rotation, Vector3 scale)
        {
            self.rotation = rotation;
            self.localScale = scale;
        }
        /// <summary>
        /// Sets the transform position and scale to the given values.
        /// </summary>
        /// <param name="position">The position to be set in the transform.</param>
        /// <param name="scale">The scale to be set in the transform.</param>
        public static void SetProperties(this Transform self, Vector3 position, Vector3 scale)
        {
            self.position = position;
            self.localScale = scale;
        }
        /// <summary>
        /// Sets the transform position and rotation to the given values.
        /// </summary>
        /// <param name="position">The position to be set in the transform.</param>
        /// <param name="rotation">The rotation to be set in the transform.</param>
        public static void SetProperties(this Transform self, Vector3 position, Quaternion rotation)
        {
            self.position = position;
            self.rotation = rotation;
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

        /// <summary>
        /// Linearly interpolates between two transforms.
        /// <para>When t = 0 returns a. When t = 1 returns b. When t = 0.5 returns the point midway between a and b.</para>
        /// </summary>
        public static void SetLerp(this Transform self, Transform a, Transform b, float t)
        {
            self.SetProperties(Vector3.Lerp(a.position, b.position, t), Quaternion.Lerp(a.rotation, b.rotation, t), Vector3.Lerp(a.localScale, b.localScale, t));
        }
    
        /// <summary>
        /// Destroys immediately the children of a transform.
        /// <para>This method is not recommended to use. Instead use 'DestroyAllChildren'."</para>
        /// </summary>
        /// <param name="exceptions">Transforms that must not be destroyed.</param>
        public static void DestroyImmediateAllChildren(this Transform self, IEnumerable<Transform> exceptions = null)
        {
            Transform[] exceptionsArray = exceptions != null ? exceptions.ToArray() : new Transform[0];
        
            while ((exceptions == null && self.childCount > 0) || (exceptions != null && self.childCount > exceptionsArray.Length))
                foreach (Transform child in self)
                    if (exceptions == null || !exceptionsArray.Contains(child))
                        Object.DestroyImmediate(child.gameObject);
        }
    
        /// <summary>
        /// Destroys the children of a transform.
        /// </summary>
        /// <param name="exceptions">Transforms that must not be destroyed.</param>
        public static void DestroyAllChildren(this Transform self, IEnumerable<Transform> exceptions = null)
        {
            Transform[] exceptionsArray = exceptions != null ? exceptions.ToArray() : new Transform[0];
        
            //while ((exceptions == null && self.childCount > 0) || (exceptions != null && self.childCount > exceptionsArray.Length))
            foreach (Transform child in self)
                if (exceptions == null || !exceptionsArray.Contains(child))
                    Object.Destroy(child.gameObject);
        }
        
    }
}
