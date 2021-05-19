using System;
using UnityEngine;
using UnityEngine.UI;
using Console = UnityEngine.Console;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class ConsoleTextUI : Console
{
    [NonSerialized] public Text textUI;

    protected override void UpdateVisuals()
    {
        if (textUI == null)
            textUI = gameObject.GetComponentRequired<Text>();
        
        textUI.enabled = show;
        
        if (!show) 
            return;
        textUI.text = fullLog;
        
    }
    
}
