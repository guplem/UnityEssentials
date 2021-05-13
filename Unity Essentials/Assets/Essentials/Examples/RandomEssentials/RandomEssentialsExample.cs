using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Essentials.Examples.RandomEssentials
{
    public class RandomEssentialsExample : MonoBehaviour
    {
        [Header("Pseudo Random")]
        [SerializeField] private KeyCode keyPseudoRandom;
        [SerializeField] private float pseudoRandomProbability = 0.25f;
        [Header("Random Boolean")]
        [SerializeField] private KeyCode keyRandomBool;
        [SerializeField] private float trueBooleanProbability = 0.5f;
        [Header("Random Float")]
        [SerializeField] private KeyCode keyRandomFloat;

        private UnityEngine.RandomEssentials randomEssentials;
    
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
            randomEssentials = new UnityEngine.RandomEssentials();
            
            //EXAMPLES:
            bool randomBool = randomEssentials.GetRandomBool(0.75f);
            int randomInt = randomEssentials.GetRandomInt(3,10);
            float randomFloat = randomEssentials.GetRandomFloat(0.5f, 0.1f);
            Vector3 randomVector3 = randomEssentials.GetRandomVector3(0.5f, 0.1f);
            Vector2 randomVector2 = randomEssentials.GetRandomVector2(0.5f, 0.1f);
            int randomSign = randomEssentials.GetRandomSign(0.4f);
            bool randomBoolTrueEnsured = randomEssentials.GetRandomBoolTrueEnsured(3, 10);
            bool randomDistributedBool = randomEssentials.GetPseudoRandomDistributedBool(5, 0.25f);

            // To make easier the single use of RandomEssentials, the object can be created and used as follows:
            randomBool = UnityEngine.RandomEssentials.GetNew().GetRandomBool();
            randomBool = UnityEngine.RandomEssentials.GetNew(123456).GetRandomBool(); // With custom seed
            
                    // This variables are not used anywhere, they exist only to help the example.
        }

        void Update()
        {
            // Random bool example
            if (Input.GetKeyDown(keyRandomBool))
            {
                bool obtainedBool = randomEssentials.GetRandomBool(trueBooleanProbability);
                if (obtainedBool) trueBooleans++; else falseBooleans++;
                Debug.Log("The gotten value is " + obtainedBool + ". Average = " + (trueBooleans*100f/(trueBooleans+falseBooleans))  + "%");
            }
            
        
            // Random float example
            if (Input.GetKeyDown(keyRandomFloat))
                Debug.Log("The gotten value is " + randomEssentials.GetRandomFloat(3.5f, 5.0f));
        
        
            // Pseudo-random distribution example
            // An image of how the results should distribute (in blue): https://gamepedia.cursecdn.com/dota2_gamepedia/8/8b/AttacksUntilNextProc25.jpg?version=459537150af02c929fd939495fa78033
            if (Input.GetKey(keyPseudoRandom))
            {
                // NOTE: It is recommended to play with the values in the inspector to better understand what is the result of the "GetPseudoRandomDistributedBool" method  
            
                bool result = randomEssentials.GetPseudoRandomDistributedBool(tryNumberSinceLastPositive, pseudoRandomProbability);

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
}