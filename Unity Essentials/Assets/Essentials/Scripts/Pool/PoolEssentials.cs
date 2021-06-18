using System.Collections.Generic;

namespace UnityEngine
{
    /// <summary>
    /// Allows GameObjects pooling by reusing pre-instantiated GameObjects
    /// </summary>
    [System.Serializable]
    public class PoolEssentials
    {
        /// <summary>
        /// A reference to the instantiated GameObjects linked to the pool.
        /// </summary>
        private List<GameObject> referencedObjects = new List<GameObject>();
        /// <summary>
        /// The prefabs of the GameObjects to be spawned for the pool.
        /// </summary>
        [Tooltip("The prefabs of the GameObjects to be spawned for the pool.")]
        public GameObject[] baseObjects = new GameObject[1];
        /// <summary>
        /// The index of the next GameObject to be activated/spawned.
        /// </summary>
        public int activeIndex { get; private set; }
        /// <summary>
        /// The index of the base object that is going to be instantiated next.
        /// <para>The index is related to the baseObjects array.</para>
        /// </summary>
        public int nextBaseObjectIndex { get; private set; }
        /// <summary>
        /// Should the selection of the next base object to instantiate be random? If false, the selection will be made in order looping through the baseObjects array.
        /// <para>If true, they will only be chosen randomly for the first instantiation, not the further reactivations.</para>
        /// </summary>
        [Tooltip("Should the selection of the next base object to instantiate be random? If false, the selection will be made in order looping through the baseObjects array.")]
        public bool randomInstantiationSequence = false;
        /// <summary>
        /// The size of the pool. How many referenced objects it han have.
        /// </summary>
        [Tooltip("The size of the pool. How many referenced objects it han have.")]
        public int size = 10;
        /// <summary>
        /// The randomness generator object used to choose the next base object to instantiate.
        /// </summary>
        private RandomEssentials randomEssentialsInstantiation = new RandomEssentials();
        /// <summary>
        /// The default position and rotation where the objects are first instantiated
        /// </summary>
        private DefaultPositionAndRotation defaultPositionAndRotation = new DefaultPositionAndRotation();

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        public PoolEssentials() { 
            activeIndex = 0;
            nextBaseObjectIndex = 0;
            referencedObjects = new List<GameObject>();
            size = 10;
        }

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        /// <param name="baseObjects">The object that will be instantiated by the pool.</param>
        /// <param name="size">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        /// <param name="randomInstantiationSequence">If false, the objects will be instantiated in the appearing order in the 'baseObjects array. If true, the order of instantiation of the pooled objects is going to be random.</param>
        /// <param name="intantiationRandomizationSeed">The seed used to randomly pick the baseObjects in the first instantiation process</param>
        public PoolEssentials(GameObject[] baseObjects, int size, bool instantiateAllAtCreation = false, bool randomInstantiationSequence = false, int intantiationRandomizationSeed = -1)
        {
            this.baseObjects = baseObjects;
            this.size = size;
            referencedObjects = new List<GameObject>();
            activeIndex = 0;
            
            this.randomInstantiationSequence = randomInstantiationSequence;
            if (intantiationRandomizationSeed != -1)
                this.randomEssentialsInstantiation = new RandomEssentials(intantiationRandomizationSeed);
            
            if (instantiateAllAtCreation)
                for (int i = 0; i < size; i++)
                    InstantiateNewAt(i);
        }
        
        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        /// <param name="baseObject">The object that will be instantiated by the pool.</param>
        /// <param name="size">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        public PoolEssentials(GameObject baseObject, int size, bool instantiateAllAtCreation = false)
        {
            this.baseObjects = new GameObject[] {baseObject};
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
        /// <param name="baseObjects">The object that will be instantiated by the pool.</param>
        /// <param name="size">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiationPosition">The position where the objects must be instantiated.</param>
        /// <param name="instantiationRotation">The rotation that the objects must have when instantiated.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        /// <param name="randomInstantiationSequence">If false, the objects will be instantiated in the appearing order in the 'baseObjects array. If true, the order of instantiation of the pooled objects is going to be random.</param>
        /// <param name="intantiationRandomizationSeed">The seed used to randomly pick the baseObjects in the first instantiation process</param>
        public PoolEssentials(GameObject[] baseObjects, int size, Vector3 instantiationPosition, Quaternion instantiationRotation, bool instantiateAllAtCreation = false, bool randomInstantiationSequence = false, int intantiationRandomizationSeed = -1) : this(baseObjects, size, instantiateAllAtCreation, randomInstantiationSequence, intantiationRandomizationSeed)
        {
            defaultPositionAndRotation = new DefaultPositionAndRotation(instantiationPosition, instantiationRotation);
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

            if ((referencedObjects.Count <= activeIndex) || (referencedObjects[activeIndex] == null))
            {
                activeIndex = activeIndex.GetLooped(size); //index = index+1<referencedObjects.Length? index+1 : 0;
                Debug.LogError(
                    "An error occured trying to spawn the GameObject using the class Pool\nBe sure that your pools are properly configured.");
                return null;
            }
            else
            {
                referencedObjects[activeIndex].transform.SetProperties(position, rotation, scale);
                if (parent != null)
                    referencedObjects[activeIndex].transform.parent = parent;
                referencedObjects[activeIndex].SetActive(true);

                GameObject goToReturn = referencedObjects[activeIndex];


                activeIndex = activeIndex.GetLooped(size); //index = index+1<referencedObjects.Length? index+1 : 0;

                return goToReturn;
            }
        }

        /// <summary>
        /// Loads an objet to be later activated/used.
        /// </summary>
        /// <param name="quantity">The amount of GameObjects that are wanted to be load (but not activated yet)</param>
        /// <param name="parent">The parent that will be set to the activated object.</param>
        public void Load(int quantity, Transform parent = null)
        {
            int remainingQuantity = Mathf.Min(size - referencedObjects.Count, quantity);
            for (int i = 0; i < size; i++)
            {
                if (InstantiateNewAt(i, parent) != null)
                    remainingQuantity--;
                if (remainingQuantity <= 0)
                    break;
            }
        }
        
        /// <summary>
        /// Instantiates an object for the pool.
        /// <para>Be aware: it will only instantiate the new GameObject if the pool size is preserved and if there are non existing GameObjects in the indicated index.</para>>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="parent">The parent that will be set to the activated object.</param>
        /// <returns></returns>
        private GameObject InstantiateNewAt(int index, Transform parent = null)
        {
            if (referencedObjects.Count >= size || (index < referencedObjects.Count && referencedObjects[index] != null))
                return null;
            
            if (index != referencedObjects.Count)
                Debug.LogWarning($"Trying to instantiate an object in the pool '{this}' but the expected index of the object should be {referencedObjects.Count} and it is {index}.");
            
            GameObject nextToInstantiate = GetNextBaseObjectToInstantiate(true);
            
            if(nextToInstantiate == null)
                return null;

            GameObject go = Object.Instantiate(nextToInstantiate, defaultPositionAndRotation.instantiationPosition, defaultPositionAndRotation.instantiationRotation, parent);
            go.SetActive(false);
            referencedObjects.Add(go);
            return go;
        }

        /// <summary>
        /// Returns the next BaseObject to be spawned
        /// </summary>
        /// <param name="register">Should the request be registered so the next time it is made, the next element is returned?</param>
        /// <returns></returns>
        private GameObject GetNextBaseObjectToInstantiate(bool register)
        {
            if (randomInstantiationSequence)
            {
                nextBaseObjectIndex = register
                    ? randomEssentialsInstantiation.GetRandomInt(baseObjects.Length)
                    : nextBaseObjectIndex;
            }
            else
            {
                nextBaseObjectIndex = register
                    ? nextBaseObjectIndex.GetLooped(baseObjects.Length)
                    : nextBaseObjectIndex;
            }
            return baseObjects[nextBaseObjectIndex];
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
        
        /// <summary>
        /// Class to handle the default position and rotation where the objects are first instantiated
        /// </summary>
        [System.Serializable]
        private class DefaultPositionAndRotation
        {
            /// <summary>
            /// The default position where the objects will be instantiated
            /// </summary>
            public Vector3 instantiationPosition = Vector3.zero;
            /// <summary>
            /// The default rotation of the new instantiated objects
            /// </summary>
            public Quaternion instantiationRotation = Quaternion.identity;

            public DefaultPositionAndRotation() { }
        
            public DefaultPositionAndRotation(Vector3 instantiationPosition, Quaternion instantiationRotation)
            {
                this.instantiationPosition = instantiationPosition;
                this.instantiationRotation = instantiationRotation;
            }
        }
    }
    

    
}