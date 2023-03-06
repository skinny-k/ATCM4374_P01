using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberFormatter
{
    public static string FormatNumber(float input)
    {
        string unformatted = input + "";

        if (unformatted.Length < 4)
        {
            return unformatted;
        }
        else
        {
            string firstThree = unformatted.Substring(0, 3);

            if (unformatted.Length <= 6)
            {
                string beforeDec = firstThree.Substring(0, unformatted.Length - 3);
                string afterDec = firstThree.Substring(unformatted.Length - 3);

                return beforeDec + "." + afterDec + "k";
            }
            else if (unformatted.Length <= 9)
            {
                string beforeDec = firstThree.Substring(0, unformatted.Length - 6);
                string afterDec = firstThree.Substring(unformatted.Length - 6);

                return beforeDec + "." + afterDec + "m";
            }
            else if (unformatted.Length <= 12)
            {
                string beforeDec = firstThree.Substring(0, unformatted.Length - 9);
                string afterDec = firstThree.Substring(unformatted.Length - 9);

                return beforeDec + "." + afterDec + "b";
            }
            else if (unformatted.Length <= 15)
            {
                string beforeDec = firstThree.Substring(0, unformatted.Length - 12);
                string afterDec = firstThree.Substring(unformatted.Length - 12);

                return beforeDec + "." + afterDec + "t";
            }

            return "ERROR";
        }
    }
}
