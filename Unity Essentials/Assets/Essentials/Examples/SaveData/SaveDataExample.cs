using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataExample : MonoBehaviour
{

    #region Data

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
    
    #endregion

    #region SaveAndLoadFunctionallity
    
    public void SaveAll()
    {
        // Simple Save to a file (without encryption)
        SaveDataManager.Save(floatNumber, "float_file");
        
        // Saving while encrypting
        SaveDataManager.Save(boolean, "boolean_file", "passwordHere");
        
        // Save of a list of a custom class without encryption
        SaveDataManager.Save(listOfDummyClasses, "listOfDummyClasses_file", "");
    }
    
    public void LoadAll()
    {
        // Simple load of a file (without encryption)
        floatNumber = SaveDataManager.Load("float_file", -1f);
        
        // Loading en encrypted file
        boolean = SaveDataManager.Load("boolean_file", false, "passwordHere");
        
        // Load of a list of a custom class without encryption
        listOfDummyClasses = SaveDataManager.Load("listOfDummyClasses_file", new List<DummyClass>(), "", null, true);
    }
    
    public void OpenSavedDataFolder()
    {
        SaveDataManager.OpenSavedDataFolder();
    }
    public void DeleteFloat()
    {
        SaveDataManager.Delete("float_file");
    }
    public void DeleteAll()
    {
        SaveDataManager.DeleteAll();
    }
    
    #endregion
}

