using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public class Pool
    {
        private List<GameObject> referencedObjects;
        public GameObject baseObject;
        private int activeIndex = 0;
        public Vector3 instantiationPosition = Vector3.zero;
        public Quaternion instantiationRotation = Quaternion.identity;
        public int size;

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        /// <param name="baseObject">The object that will be instantiated by the pool.</param>
        /// <param name="size">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        public Pool(GameObject baseObject, int size, bool instantiateAllAtCreation = false)
        {
            this.baseObject = baseObject;
            this.size = size;
            referencedObjects = new List<GameObject>();
            activeIndex = 0;
            
            if (instantiateAllAtCreation)
                for (int i = 0; i < size; i++)
                    InstantiateNewAt(i);
        }

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        /// <param name="baseObject">The object that will be instantiated by the pool.</param>
        /// <param name="size">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiationPosition">The position where the objects must be instantiated.</param>
        /// <param name="instantiationRotation">The rotation that the objects must have when instantiated.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        public Pool(GameObject baseObject, int size, Vector3 instantiationPosition, Quaternion instantiationRotation, bool instantiateAllAtCreation = false) : this(baseObject, size, instantiateAllAtCreation)
        {
            this.instantiationPosition = instantiationPosition;
            this.instantiationRotation = instantiationRotation;
        }

        /// <summary>
        /// Activates an object from the pool.
        /// <para>The activated object will be chosen dynamically looping between all the objects in the pool.</para>
        /// </summary>
        /// <param name="position">The position where the objects must be moved to.</param>
        /// <param name="rotation">The rotation that must be set to the object.</param>
        /// <param name="scale">The scale that must be set to the object.</param>
        /// <param name="parent">The parent that will be set to the activated object.</param>
        public GameObject Spawn(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null)
        {
            if (activeIndex>=referencedObjects.Count || referencedObjects[activeIndex] == null)
                InstantiateNewAt(activeIndex);

            referencedObjects[activeIndex].transform.SetProperties(position, rotation, scale);
            if (parent != null)
                referencedObjects[activeIndex].transform.parent = parent;
            referencedObjects[activeIndex].SetActive(true);
            
            GameObject goToReturn = referencedObjects[activeIndex];
            
            activeIndex = activeIndex.GetLooped(size); //index = index+1<referencedObjects.Length? index+1 : 0;

            return goToReturn;
        }
        
        //Only instantiates new elements if the pool size is preserved and if there are non existing GameObjects in the indicated index.
        private GameObject InstantiateNewAt(int index)
        {
            if (referencedObjects.Count >= size || (index < referencedObjects.Count && referencedObjects[index] != null))
                return null;

            GameObject go = Object.Instantiate(baseObject, instantiationPosition, instantiationRotation);
            go.SetActive(false);
            referencedObjects.Add(go);
            return go;
        }

        /// <summary>
        /// Disables the given gameObject if it belongs to the pool.
        /// </summary>
        /// <param name="gameObject">The GameObject to disable.</param>
        /// <returns>The disabled object if it belong to the pool. Null otherwise.</returns>
        public GameObject Disable(GameObject gameObject)
        {
            if (referencedObjects.Contains(gameObject))
            {
                gameObject.SetActive(false);
                return gameObject; 
            }
            else
            {
                Debug.LogWarning("Trying to disable an object ("+gameObject.name+") from a pool that doesn't belong to.");
                return null;
            }
        }
        
        /// <summary>
        /// Disables the gameObject in the given index
        /// </summary>
        /// <param name="gameObjectIndexInPool">The index in the pool of the GameObject to disable.</param>
        /// <returns>The disabled object.</returns>
        public GameObject Disable(int gameObjectIndexInPool)
        {
            return Disable(referencedObjects[gameObjectIndexInPool]);
        }
    }
}