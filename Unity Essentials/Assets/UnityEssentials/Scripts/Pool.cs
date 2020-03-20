using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
    public class Pool
    {
        private GameObject[] referencedObjects;
        private GameObject baseObject;
        private int index = 0;
        private Vector3 instantiationPosition = Vector3.zero;
        private Quaternion instantiationRotation = Quaternion.identity;

        public Pool(GameObject baseObjectPrefab, int poolSize, bool instantiateAllAtCreation = false)
        {
            baseObject = baseObjectPrefab;
            referencedObjects = new GameObject[poolSize];
            index = 0;
            
            if (instantiateAllAtCreation)
                for (int i = 0; i < poolSize; i++)
                    Instantiate(i);
        }
        
        public Pool(GameObject baseObjectPrefab, int poolSize, Vector3 instantiationPosition, Quaternion instantiationRotation, bool instantiateAllAtCreation = false) : this(baseObjectPrefab, poolSize, instantiateAllAtCreation)
        {
            this.instantiationPosition = instantiationPosition;
            this.instantiationRotation = instantiationRotation;
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            if (referencedObjects[index] == null)
                Instantiate(index);

            referencedObjects[index].transform.SetProperties(position, rotation);
            referencedObjects[index].SetActive(true);
            GameObject returnObject = referencedObjects[index];
            
            index = index+1<referencedObjects.Length? index+1 : 0;

            return returnObject;
        }

        private void Instantiate(int referencePosition)
        {
            GameObject go = Object.Instantiate(baseObject, instantiationPosition, instantiationRotation);
            go.SetActive(false);
            referencedObjects[referencePosition] = go;
        }
    }
}