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
            equation = string.Format("({0} - {1}) = X", results, number1);
            EquationStruct resultRow = new EquationStruct(equation, answer);
            result.Add(resultRow);
        }

        return result;

        //string res = "";
        //int total = 0;

        //int num1 = 0;
        //int num2 = 0;
        //int num3 = 0;
        //int RandOperation = rand.Next(0, 2);

        //if (RandOperation == 0)
        //{
        //    while (total <= maxResult)
        //    {
        //        num1 = rand.Next(1, 20);
        //        num2 = rand.Next(1, 20);
        //        num3 = rand.Next(1, 50);

        //        if (num1 < num2)
        //        {
        //            int temp = num1;
        //            num1 = num2;
        //            num2 = temp;
        //        }
        //        res = string.Format("({0} / {1}) + {2} = X", num1, num2, num3);
        //        total = num1 / num2 + num3;
        //    }

        //}
        //else
        //{
        //    while (total <= maxResult)
        //    {
        //        num1 = rand.Next(1, 20);
        //        num2 = rand.Next(1, 20);
        //        num3 = rand.Next(1, 50);
        //        res = string.Format("({0} * {1}) - {2} = X", num1, num2, num3);
        //        total = num1 * num2 - num3;
        //    }
        //}

        //EquationStruct resultRow = new EquationStruct(res, total);
        //result.Add(resultRow);

        //return result;
    }
}
