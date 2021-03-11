using System.Collections.Generic;

namespace UnityEngine
{

    [System.Serializable]
    public class Pool
    {
        private List<GameObject> referencedObjects = new List<GameObject>();
        public GameObject[] baseObjects = new GameObject[1];
        public int activeIndex { get; private set; }
        public int lastBaseObjectRegisteredIndex { get; private set; }
        public bool randomInstantiationSequence = false; // If ture, they will only be chosen randomly for the first initialization, not the further respawns
        public int size = 10;
        public RandomEssentials randomEssentialsInstantiation = new RandomEssentials();
        private DefaultPositionAndRotation defaultPositionAndRotation = new DefaultPositionAndRotation();

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        public Pool() { 
            activeIndex = 0;
            lastBaseObjectRegisteredIndex = 0;
            referencedObjects = new List<GameObject>();
            size = 10;
        }

        /// <summary>
        /// Creates a Pool instance.
        /// </summary>
        /// <param name="baseObject">The object that will be instantiated by the pool.</param>
        /// <param name="size">The maximum number of objects that can be instantiated at the same time.</param>
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        public Pool(GameObject[] baseObjects, int size, bool instantiateAllAtCreation = false)
        {
            this.baseObjects = baseObjects;
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
        /// <param name="instantiateAllAtCreation">If the pool should instantiate all the objects in the scene right away (true) or if they should be instantiated when they are needed (false, default value).</param>
        public Pool(GameObject baseObjects, int size, bool instantiateAllAtCreation = false)
        {
            this.baseObjects = new GameObject[] {baseObjects};
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
        /// <param name="randomInstantiationSequence">If false, the objects will be instantiated in the appearing order in the 'baseObjects array. If true, the order of instantiation of the pooled objects is going to be random./param>
        /// <param name="intantiationRandomizationSeed">The seed used to randomly pick the baseObjects in the first instantiation process</param>
        public Pool(GameObject[] baseObjects, int size, Vector3 instantiationPosition, Quaternion instantiationRotation, bool instantiateAllAtCreation = false, bool randomInstantiationSequence = false, int intantiationRandomizationSeed = -1) : this(baseObjects, size, instantiateAllAtCreation)
        {
            defaultPositionAndRotation = new DefaultPositionAndRotation(instantiationPosition, instantiationRotation);
            this.randomInstantiationSequence = randomInstantiationSequence;
            if (intantiationRandomizationSeed != -1)
                this.randomEssentialsInstantiation = new RandomEssentials(intantiationRandomizationSeed);
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
        
        //Only instantiates new elements if the pool size is preserved and if there are non existing GameObjects in the indicated index.
        private GameObject InstantiateNewAt(int index, Transform parent = null)
        {
            if (referencedObjects.Count >= size || (index < referencedObjects.Count && referencedObjects[index] != null))
                return null;
            
            GameObject nextToInstantiate = GetNextBaseObjectToInstantiate(true);
            
            if(nextToInstantiate == null)
                return null;

            GameObject go = Object.Instantiate(nextToInstantiate, defaultPositionAndRotation.instantiationPosition, defaultPositionAndRotation.instantiationRotation, parent);
            go.SetActive(false);
            referencedObjects.Add(go);
            return go;
        }

        // Returns the next BaseObject to be spawned
        private GameObject GetNextBaseObjectToInstantiate(bool register)
        {
            if (randomInstantiationSequence)
            {
                lastBaseObjectRegisteredIndex = register
                    ? randomEssentialsInstantiation.GetRandomInt(baseObjects.Length)
                    : lastBaseObjectRegisteredIndex;
            }
            else
            {
                lastBaseObjectRegisteredIndex = register
                    ? lastBaseObjectRegisteredIndex.GetLooped(baseObjects.Length)
                    : lastBaseObjectRegisteredIndex;
            }

            return baseObjects[lastBaseObjectRegisteredIndex];
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
        
        [System.Serializable]
        public class DefaultPositionAndRotation
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