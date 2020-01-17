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

    public bool GetPseudoRandomBool(int tryNumber, float addedProbabilityPerTry) // tryNumber > 0 // addedProbabilityPerTry = [0, 1]
    {
        return Sample() <= addedProbabilityPerTry * tryNumber;
    }
    
    public bool GetPseudoRandomBool(int tryNumber, int maxNumberOfTries) // tryNumber > 0 // maxNumberOfTries > 0
    {
        return Sample() <= (float)tryNumber/(float)maxNumberOfTries;
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
