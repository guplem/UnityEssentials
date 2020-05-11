using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class test
{
    [MenuItem("TEST/TEST")]
    public static void FizzBuzz()
    {
        for (int i = 1; i <= 100; i++) {
            string output = "";
            output = AddTextIfTrue(output, i.IsMultipleOf(3f), "Fizz");
            output = AddTextIfTrue(output, i.IsMultipleOf(5f), "Buzz");
            output = AddTextIfTrue(output, (output.Length == 0), i.ToString());
            Debug.Log(output);
        }
    }

    private static string AddTextIfTrue(string output, bool check, string text)
    {
        output += check ? text : "";
        return output;
    }


}
