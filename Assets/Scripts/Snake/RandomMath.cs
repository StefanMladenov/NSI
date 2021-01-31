using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomMath 
{
    public struct EquationStruct
    {
        public string equation;
        public int result;

        public EquationStruct(string equation, int result)
        {
            this.equation = equation;
            this.result = result;
        }
    }

    public static List<EquationStruct> GenerateRandomEquation(int maxResult)
    {
        List<EquationStruct> result = new List<EquationStruct>();

        int number1 = 0;
        int answer = 0;
        int results = 0;
        string equation = "";

        System.Random rand = new System.Random();

        for (int i = 0; i < 20; i++)
        {
            results = rand.Next(1, maxResult);

            number1 = rand.Next(0, maxResult);

            answer = results - number1;
            equation = string.Format("{0} - {1} = X", results, number1);
            EquationStruct resultRow = new EquationStruct(equation, answer);
            result.Add(resultRow);
        }

        return result;
    }
}
