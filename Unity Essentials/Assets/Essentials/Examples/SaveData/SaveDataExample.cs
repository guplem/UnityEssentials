using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataExample : MonoBehaviour
{
    [SerializeField] private float floatNumber;
    [SerializeField] private bool boolean;
    [SerializeField] private List<DummyClass> listOfDummyClasses = new List<DummyClass>();
    [Serializable]
    public class DummyClass
    {
        public string stringText;
        public int intNumber;
        public Vector3 vector;
    }

    public void Save()
    {
        // Simple Save to a file (without encryption)
        SavedDataManager.Save(floatNumber, "float_file");
        
        // Saving while encrypting
        SavedDataManager.Save(boolean, "boolean_file", "passwordHere");
        
        // Save of a list of a custom class without encryption
        SavedDataManager.Save(listOfDummyClasses, "listOfDummyClasses_file", "");
    }
    
    public void Load()
    {
        // Simple load of a file (without encryption)
        floatNumber = SavedDataManager.Load("float_file", -1f);
        
        // Loading en encrypted file
        boolean = SavedDataManager.Load("boolean_file", false, "passwordHere");
        
        // Load of a list of a custom class without encryption
        listOfDummyClasses = SavedDataManager.Load("listOfDummyClasses_file", new List<DummyClass>(), "");
    }
    

    
}

