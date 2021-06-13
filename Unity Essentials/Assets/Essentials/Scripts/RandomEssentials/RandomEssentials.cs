using System;

namespace UnityEngine
{
    /// <summary>
    /// An easier to use and a feature-rich class to generate pseudo-random results.
    /// </summary>
    public class RandomEssentials : System.Random
    {

        #region Constructors
        
        /// <summary>
        /// Creates a pseudo-random number generator, which is an algorithm that produces a sequence of numbers that meet certain statistical requirements for randomness.
        /// <para>Initializes pseudo-random number generator using a default seed value.</para>
        /// </summary>
        public RandomEssentials()
        {
        }

        /// <summary>
        /// Creates a pseudo-random number generator, which is an algorithm that produces a sequence of numbers that meet certain statistical requirements for randomness.
        /// <para>Initializes pseudo-random number generator, using the specified seed value.</para>
        /// <para>Using a custom seed ensures that the generated results will be the same (in the same order) all the times the same seed is used.</para>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used. </param>
        /// </summary>
        public RandomEssentials(int seed) : base(seed)
        {
        }
        
        /// <summary>
        /// Creates a pseudo-random number generator, which is an algorithm that produces a sequence of numbers that meet certain statistical requirements for randomness.
        /// <para>Initializes pseudo-random number generator using a default seed value.</para>
        /// </summary>
        /// <returns>A new instance a pseudo-random number generator of type "RandomEssentials".</returns>
        public static RandomEssentials GetNew()
        {
            return new RandomEssentials();
        }
        
        /// <summary>
        /// Creates a pseudo-random number generator, which is an algorithm that produces a sequence of numbers that meet certain statistical requirements for randomness.
        /// </summary>
        /// <param name="seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used. </param>
        /// <returns>A new instance a pseudo-random number generator of type "RandomEssentials".</returns>
        public static RandomEssentials GetNew(int seed)
        {
            return new RandomEssentials(seed);
        }
        
        #endregion

        #region Int

        /// <summary>
        /// <para>Returns a non-negative random integer between 0 (included) and Int32.MaxValue (excluded).</para>
        /// <para>Int32.MaxValue = 2147483647</para>
        /// </summary>
        /// <returns>Random integer in range [0, Int32.MaxValue)</returns>
        public int GetRandomInt()
        {
            return Next();
        }

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="exclusiveMaximum">The exclusive upper bound of the random number to be generated. It must be greater than or equal to 0.</param>
        /// <returns>Random integer in range [0, exclusiveMaximum)</returns>
        public int GetRandomInt(int exclusiveMaximum)
        {
            return Next(exclusiveMaximum);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="inclusiveMinimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="exclusiveMaximum">The exclusive upper bound of the random number returned. It must be greater than or equal to inclusiveMinimum.</param>
        /// <returns>Random integer in range [inclusiveMinimum, exclusiveMaximum)</returns>
        public int GetRandomInt(int inclusiveMinimum, int exclusiveMaximum)
        {
            return Next(inclusiveMinimum, exclusiveMaximum);
        }

        #endregion

        #region Sign

        /// <summary>
        /// Returns a random sign as -1 or +1.
        /// </summary>
        /// <param name="negativeProbability">The probability of returning a -1 instead of a 1. It must be within the range [0, 1]</param>
        /// <returns>A random 1 or -1</returns>
        public int GetRandomSign(float negativeProbability = 0.5f)
        {
            return GetRandomBool(negativeProbability) ? -1 : 1;
        }

        #endregion

        #region Bool

        /// <summary>
        /// Returns a random bool.
        /// </summary>
        /// <param name="probability">The probability of returning a true. It must be within the range [0, 1]</param>
        /// <returns>A random true or false</returns>
        public bool GetRandomBool(float probability = 0.5f)
        {
            return NextDouble() >= 1 - probability;
        }

        /// <summary>
        /// Returns a random bool that will be true before the maximum number of tries is exceeded.
        /// </summary>
        /// <param name="tryNumberSinceLastPositive">The number of the try since the last true was returned. It must be greater than 0. It should increase by one on each try and be restarted when a true is returned.</param>
        /// <param name="maxNumberOfTries">The maximum number of tries than can be done to ensure a true return. It should be greater than or equal to tryNumberSinceLastPositive.</param>
        /// <returns>A random bool.</returns>
        public bool GetRandomBoolTrueEnsured(int tryNumberSinceLastPositive, int maxNumberOfTries)
        {
            return GetRandomBool((float) tryNumberSinceLastPositive / (float) maxNumberOfTries);
        }

        /// <summary>
        /// Returns a random bool using pseudo-random distribution.
        /// <para>The chances of returning a 'true' increases every time it does not occur, but is lower at the first tries.</para>
        /// <para>Be aware that the resulting probability using this method won't be exactly the same as the given as parameter (but it will be approximated).</para>
        /// </summary>
        /// <param name="tryNumberSinceLastPositive">The number of the try since the last true was returned. It must be greater than 0. It should increase by one on each try and be restarted when a true is returned.</param>
        /// <param name="probability">The probability of returning a true. It must be within the range [0, 1]</param>
        /// <returns>A random bool.</returns>
        public bool GetPseudoRandomDistributedBool(int tryNumberSinceLastPositive, float probability)
        {
            return GetRandomBool(Mathf.Pow(probability, 1.72689f) * tryNumberSinceLastPositive);

            // An image of how the results should distribute (in blue): https://gamepedia.cursecdn.com/dota2_gamepedia/8/8b/AttacksUntilNextProc25.jpg?version=459537150af02c929fd939495fa78033

            /* The true formula to calculate each try's probability is: "C * tryNumber"
            // In this method's implementation, "C" is calculated using the formula: "probability^1.72689"
            // However, the accuracy of the results could be improved by using this table instead of the previous calculus:
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

        #endregion

        #region Float

        /// <summary>
        /// Returns a random float between 0 (included) and 1 (excluded)
        /// </summary>
        /// <returns>Random float in range [0, 1)</returns>
        public float GetRandomFloat()
        {
            return Convert.ToSingle(NextDouble());
        }

        /// <summary>
        /// Returns a random float that is less than the specified maximum.
        /// </summary>
        /// <param name="exclusiveMaximum">The exclusive upper bound of the random number to be generated. It must be greater than or equal to 0.</param>
        /// <returns>Random integer in range [0, exclusiveMaximum)</returns>
        public float GetRandomFloat(float exclusiveMaximum)
        {
            return Convert.ToSingle(NextDouble() * exclusiveMaximum);
        }

        /// <summary>
        /// Returns a random float that is within a specified range.
        /// </summary>
        /// <param name="inclusiveMinimum">The inclusive lower bound of the random number returned.</param>
        /// <param name="exclusiveMaximum">The exclusive upper bound of the random number returned. It must be greater than or equal to inclusiveMinimum.</param>
        /// <returns>Random float in range [inclusiveMinimum, exclusiveMaximum)</returns>
        public float GetRandomFloat(float inclusiveMinimum, float exclusiveMaximum)
        {
            return Convert.ToSingle(NextDouble() * (exclusiveMaximum - inclusiveMinimum)) + inclusiveMinimum;
        }

        #endregion

        #region Vectors

        /// <summary>
        /// Returns a random Vector3 with each parameter within a specified range.
        /// </summary>
        /// <param name="inclusiveMinimum">The inclusive lower bound of each parameter.</param>
        /// <param name="exclusiveMaximum">The exclusive upper bound of each parameter. It must be greater than or equal to inclusiveMinimum.</param>
        /// <returns>Random Vector3 with each parameter in range [inclusiveMinimum, exclusiveMaximum)</returns>
        public Vector3 GetRandomVector3(float inclusiveMinimum, float exclusiveMaximum)
        {
            return new Vector3(GetRandomFloat(inclusiveMinimum, exclusiveMaximum), GetRandomFloat(inclusiveMinimum, exclusiveMaximum), GetRandomFloat(inclusiveMinimum, exclusiveMaximum));
        }

        /// <summary>
        /// Returns a random Vector2 with each parameter within a specified range.
        /// </summary>
        /// <param name="inclusiveMinimum">The inclusive lower bound of each parameter.</param>
        /// <param name="exclusiveMaximum">The exclusive upper bound of each parameter. It must be greater than or equal to inclusiveMinimum.</param>
        /// <returns>Random Vector2 with each parameter in range [inclusiveMinimum, exclusiveMaximum)</returns>
        public Vector2 GetRandomVector2(float inclusiveMinimum, float exclusiveMaximum)
        {
            return new Vector2(GetRandomFloat(inclusiveMinimum, exclusiveMaximum), GetRandomFloat(inclusiveMinimum, exclusiveMaximum));
        }

        #endregion

        #region Bytes

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="bytesArray">The array to be filled with random numbers.</param>
        public void SetRandomBytes(Byte[] bytesArray)
        {
            NextBytes(bytesArray);
        }
        
        #endregion

        #region Geometry
        
        /// <summary>
        /// Returns a random point inside a circle.
        /// </summary>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public Vector2 GetPointInsideCircle(Vector2 center, float radius = 1f)
        {
            double angle = this.NextDouble() * 2 * Math.PI;
            double r = radius * Math.Sqrt(NextDouble());
            double x = center.x + r * Math.Cos(angle);
            double y = center.y + r * Math.Sin(angle);
            return new Vector2( Convert.ToSingle(x), Convert.ToSingle(y) );
        }	
        
        /// <summary>
        /// Returns a random point inside a circle with center (0, 0).
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public Vector2 GetPointInsideCircle( float radius = 1f)
        {
            return GetPointInsideCircle(Vector2.zero, radius);
        }	
        
        
        /// <summary>
        /// Returns a random point on a circle.
        /// </summary>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public Vector2 GetPointOnCircle(Vector2 center, float radius = 1f)
        {
            double angle = 2.0 * Math.PI * this.NextDouble();
            double x = center.x + radius * Math.Cos(angle);
            double y = center.y + radius * Math.Sin(angle);
            return new Vector2( Convert.ToSingle(x), Convert.ToSingle(y) );
        }	
        
        /// <summary>
        /// Returns a random point inside a circle with center (0, 0).
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns></returns>
        public Vector2 GetPointOnCircle( float radius = 1f)
        {
            return GetPointInsideCircle(Vector2.zero, radius);
        }	
        
        // Similar methods to the ones Unity's Random class has
        //To-do: public Vector3 GetPointInsideSphere(float radius = 1){}	//Returns a random point inside a sphere
        //To-do: public Vector3 GetPointOnSphere(float radius = 1){}	//Returns a random point on the surface of a sphere
        
        // Similar methods to the ones Unity's Random class has
        //To-do: public Quaternion GetRotation(){}	//Returns a random rotation
        //To-do: public Quaternion GetRotationUniform(){}	//Returns a random rotation with uniform distribution

        #endregion

        #region Color

        public Color GetColorHSV(float hueMin = 0.0f, float hueMax = 1f, float saturationMin = 0.0f, float saturationMax = 1f, float valueMin = 0.0f, float valueMax = 1f, float alphaMin = 1f, float alphaMax = 1f)
        {
            Color rgb = Color.HSVToRGB(Mathf.Lerp(hueMin, hueMax, GetRandomFloat()), Mathf.Lerp(saturationMin, saturationMax, GetRandomFloat()), Mathf.Lerp(valueMin, valueMax, GetRandomFloat()), true);
            rgb.a = Mathf.Lerp(alphaMin, alphaMax, GetRandomFloat());
            return rgb;
        }
        
        #endregion


    }
}