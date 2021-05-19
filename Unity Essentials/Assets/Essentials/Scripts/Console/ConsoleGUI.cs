using UnityEngine;

[ExecuteInEditMode]
public class ConsoleGUI : Console
{
    private void OnGUI()
    {
        if (!show) 
            return;
            
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
        GUI.TextArea(new Rect(10, 10, 540, 370), fullLog);
    }
    
    protected override void UpdateVisuals()
    {
        // -- Visual update done at "OnGUI"
    }
    
}
