using UnityEngine;

public class ConsoleExample : MonoBehaviour
{
    [SerializeField] private Console[] consoles;
    
    public void LogSomething()
    {
        int randomInt = Random.Range(0,5);
        switch (randomInt)
        {
            case 0: Debug.Log("I really have no idea what to write here...");
                break;
            case 1: Debug.Log("I hope you are enjoying this asset!");
                break;
            case 2: Debug.LogWarning("Will anybody ready this?... ever? (this is a warning)");
                break;
            case 3: Debug.LogError("Maaaan...., I really wish to finish this feature soon... (this is an error)");
                break;
            case 4: Debug.Log("I hope you are having a nice day!  : )");
                break;
        }
    }

    public void ClearAll()
    {
        foreach (Console console in consoles)
        {
            console.Clear();
        }
    }
}
