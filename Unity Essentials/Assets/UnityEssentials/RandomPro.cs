using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPro : System.Random
{
    public RandomPro() { }

    public RandomPro(int seed) : base(seed) { }

    
    
    
    public int GetRandomInt() // Range = [0, Int32.MaxValue)
    {
        return Next();
    }

    public int GetRandomInt(int exclusiveMaximum) // Range = [0, exclusiveMaximum)
    {
        return Next(exclusiveMaximum);
    }
    
    public int GetRandomInt(int inclusiveMinimum, int exclusiveMaximum) // Range = [inclusiveMinimum, exclusiveMaximum)
    {
        return Next(inclusiveMinimum, exclusiveMaximum);
    }

    
    
    
    public bool GetRandomBool(float probability = 0.5f) // probability = [0, 1]
    {
        return Sample() < probability;
    }

    // Be aware that the resulting probability using this method won't be exactly the same as the given as parameter (but it will be approximated)
    public bool GetPseudoRandomBool(int tryNumber, float probability) // tryNumber > 0 // addedProbabilityPerTry = [0, 1]
    {
        return GetRandomBool(Mathf.Pow(probability, 1.72689f) * tryNumber);
        
        /* The formula to calculate each try probability is: "C * tryNumber"
        // In this method's implementation, "C" is calculated using the formula: "probability^1.72689"
        // However, the accuracy of the results could be improved by using this table:
        
            Probability    Associated "C"	                           
            5%	           0.003801658303553139101756466
            10%	           0.014745844781072675877050816
            15%	           0.032220914373087674975117359
            20%	           0.055704042949781851858398652
            25%	           0.084744091852316990275274806
            30%	           0.118949192725403987583755553
            35%	           0.157983098125747077557540462
            40%	           0.201547413607754017070679639
            45%	           0.249306998440163189714677100
            50%	           0.302103025348741965169160432
            55%	           0.360397850933168697104686803
            60%	           0.422649730810374235490851220
            65%	           0.481125478337229174401911323
            70%	           0.571428571428571428571428572
            75%	           0.666666666666666666666666667
            80%	           0.750000000000000000000000000
            85%	           0.823529411764705882352941177
            90%	           0.888888888888888888888888889
            95%	           0.947368421052631578947368421
            
         */
    }
    
    public bool GetPseudoRandomBool(int tryNumber, int maxNumberOfTries) // tryNumber > 0 // maxNumberOfTries > 0
    {
        return GetRandomBool((float) tryNumber / (float) maxNumberOfTries);
    }
    
    
    
    
    public float GetRandomFloat() // Range [0.0, 1.0)
    {
        return Convert.ToSingle(NextDouble());
    }
    
    public float GetRandomFloat(float exclusiveMaximum) // Range = [0.0, exclusiveMaximum)
    {
        return Convert.ToSingle(NextDouble()*exclusiveMaximum);
    }
    
    public float GetRandomFloat(float inclusiveMinimum, float exclusiveMaximum) // Range = [inclusiveMinimum, exclusiveMaximum)
    {
        return Convert.ToSingle(NextDouble()*(exclusiveMaximum-inclusiveMinimum)) + inclusiveMinimum;
    }
    
    


    public void GetRandomBytes(Byte[] buffer)
    {
        NextBytes(buffer);
    }
    
}
