using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Pool
    {
        private GameObject[] referencedObjects;
        public GameObject baseObject;
        private int index = 0;
        public Vector3 instantiationPosition = Vector3.zero;
        public Quaternion instantiationRotation = Quaternion.identity;

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        /// <param name="baseObject">The object that will be instantiated by the pool.</param>
        /// <param name="poolSize">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        public Pool(GameObject baseObject, int poolSize, bool instantiateAllAtCreation = false)
        {
            this.baseObject = baseObject;
            referencedObjects = new GameObject[poolSize];
            index = 0;
            
            if (instantiateAllAtCreation)
                for (int i = 0; i < poolSize; i++)
                    Instantiate(i);
        }

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        /// <param name="baseObject">The object that will be instantiated by the pool.</param>
        /// <param name="poolSize">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiationPosition">The position where the objects must be instantiated.</param>
        /// <param name="instantiationRotation">The rotation that the objects must have when instantiated.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        public Pool(GameObject baseObject, int poolSize, Vector3 instantiationPosition, Quaternion instantiationRotation, bool instantiateAllAtCreation = false) : this(baseObject, poolSize, instantiateAllAtCreation)
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
        public GameObject Spawn(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            if (referencedObjects[index] == null)
                Instantiate(index);

            referencedObjects[index].transform.SetProperties(position, rotation, scale);
            referencedObjects[index].SetActive(true);
            GameObject returnObject = referencedObjects[index];
            
            index = index.GetLooped(referencedObjects.Length); //index = index+1<referencedObjects.Length? index+1 : 0;
            
            return returnObject;
        }
        
        private GameObject Instantiate(int referenceIndex)
        {
            GameObject go = Object.Instantiate(baseObject, instantiationPosition, instantiationRotation);
            go.SetActive(false);
            referencedObjects[referenceIndex] = go;
            return go;
        }
    }
}