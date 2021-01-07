using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EasyRandomExample : MonoBehaviour
{
    [Header("Pseudo Random")]
    [SerializeField] private KeyCode keyPseudoRandom;
    [SerializeField] private float pseudoRandomProbability = 0.25f;
    [Header("Random Boolean")]
    [SerializeField] private KeyCode keyRandomBool;
    [SerializeField] private float trueBooleanProbability = 0.5f;
    [Header("Random Float")]
    [SerializeField] private KeyCode keyRandomFloat;

    private EasyRandom easyRandom;
    
    // Pseudo random debugging
    private readonly List<bool> pseudoRandomResults = new List<bool>();
    private readonly List<int> numberOfTriesToGetAPositive = new List<int>();
    private int maximumNumberOfTriesToGetAPositive = 0;
    private int tryNumberSinceLastPositive = 0;
    
    //Random booleans debugging
    int trueBooleans = 0;
    int falseBooleans = 0;
    
    private void Start()
    {
        easyRandom = new EasyRandom();
        
        //EXAMPLES: 
        //easyRandom.GetRandomBool(0.75f);
        //easyRandom.GetRandomInt(3,10);
        //easyRandom.GetRandomFloat(0.5f, 0.1f);
        //easyRandom.GetRandomVector3(0.5f, 0.1f);
        //easyRandom.GetRandomVector2(0.5f, 0.1f);
        //easyRandom.GetRandomSign(0.4f);
        //easyRandom.GetRandomBoolTrueEnsured(3, 10);
        //easyRandom.GetPseudoRandomDistributedBool(5, 0.25f);
    }

    void Update()
    {
        // Random bool example
        if (Input.GetKeyDown(keyRandomBool))
        {
            bool obtainedBool = easyRandom.GetRandomBool(trueBooleanProbability);
            if (obtainedBool) trueBooleans++; else falseBooleans++;
            Debug.Log("The gotten value is " + obtainedBool + ". Average = " + (trueBooleans*100f/(trueBooleans+falseBooleans))  + "%");
        }
            
        
        // Random float example
        if (Input.GetKeyDown(keyRandomFloat))
            Debug.Log("The gotten value is " + easyRandom.GetRandomFloat(3.5f, 5.0f));
        
        
        // Pseudo-random distribution example
        // An image of how the results should distribute (in blue): https://gamepedia.cursecdn.com/dota2_gamepedia/8/8b/AttacksUntilNextProc25.jpg?version=459537150af02c929fd939495fa78033
        if (Input.GetKey(keyPseudoRandom))
        {
            // NOTE: It is recommended to play with the values in the inspector to better understand what is the result of the "GetPseudoRandomDistributedBool" method  
            
            bool result = easyRandom.GetPseudoRandomDistributedBool(tryNumberSinceLastPositive, pseudoRandomProbability);

            // Stats debugging (for a better understanding)
            pseudoRandomResults.Add(result);
            if (result) {
                numberOfTriesToGetAPositive.Add(tryNumberSinceLastPositive);
                if (tryNumberSinceLastPositive > maximumNumberOfTriesToGetAPositive)
                    maximumNumberOfTriesToGetAPositive = tryNumberSinceLastPositive;
                tryNumberSinceLastPositive = 0;
            } else {
                tryNumberSinceLastPositive++;
            }
            int numberOfPositiveResults = pseudoRandomResults.Count(r => r);
            
            //Console report
            string averageQtyOfTriesForPositive = "Average needed quantity of tries to get a positive: " + (numberOfTriesToGetAPositive.Count > 0 ? (Mathf.Round((float) (numberOfTriesToGetAPositive.Average()*100))/100 + " ("+ Mathf.Round((float) (100/numberOfTriesToGetAPositive.Average()*100))/100 +"%).") : "-.") ;
            string percentagePositiveValues = "Percentage of positive values: " + Mathf.Round((float)numberOfPositiveResults/pseudoRandomResults.Count*100*100)/100.0f + "%.";
            string triesForPositive = "Maximum number of tries needed to get a positive: " + maximumNumberOfTriesToGetAPositive + ".";
            Debug.Log("Result: " + result + ". " + averageQtyOfTriesForPositive + " " + percentagePositiveValues + " " + triesForPositive);
        }

    }

}