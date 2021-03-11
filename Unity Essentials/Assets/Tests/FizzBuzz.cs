using Essentials.Scripts.Extensions;
using UnityEditor;
using UnityEngine;

namespace Tests
{
    public static class FizzBuzz
    {
        [MenuItem("TESTS/FizzBuzz")]
        public static void DoFizzBuzz()
        {
            for (int i = 1; i <= 100; i++) {
                string carryText = "";
                carryText = AddTextIfTrue(carryText, i.IsMultipleOf(3f), "Fizz");
                carryText = AddTextIfTrue(carryText, i.IsMultipleOf(5f), "Buzz");
                carryText = AddTextIfTrue(carryText, (carryText.Length == 0), i.ToString());
                Debug.Log(carryText);
            }
        }

        private static string AddTextIfTrue(string carryText, bool check, string text)
        {
            carryText += check ? text : "";
            return carryText;
        }
    
    }
}
