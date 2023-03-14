using System;

namespace ObjectIO;
public static class Parser
{
    public static object ParseValue(string val, Dictionary<string, object> vMetadata = null)
    {
        object result = null;

        string[] pieces = val.Split('+');
        int indexPieces = 0;
        foreach(string piece in pieces)
        {
            if (piece.StartsWith("$"))
            {
                object b = vMetadata[piece.Trim().Remove(0,1)];
                if (result != null)
                    result = Concat(result, b);
                else
                {
                    result = b;
                }
            }
            else
            {
                object b = Valuetion(piece.Trim());
                if (result != null)
                    result = Concat(result, b);
                else
                {
                    result = b;
                }
            }
            indexPieces++;
        }

        return result;
    }
    public static object Valuetion(string val)
    {
        object result = null;

        if (val.StartsWith('"') && val.EndsWith('"'))
        {
            result = val.Substring(1, val.Length - 1);
        }
        else if (int.TryParse(val, out int rNum))
        {
            result = rNum;
        }
        else if (bool.TryParse(val, out bool rBool))
        {
            result = rBool;
        }
        else
        {
            throw new Exception("[Valuetion]: Value cannot be recognized!");
        }

        return result;
    }
    public static object Concat(object a, object b)
    {
        object result = null;

        if (a is int && b is int)
        {
            result = (int)a + (int)b;
        }
        else if (a is string && b is string)
        {
            result = (string)a + (string)b;
        }
        else if (a is bool && b is bool)
        {
            int num = (int)a + (int)b;
            if (num >= 1)
                result = true;
            else if (num <= 0)
                result = false;
        }
        else
        {
            throw new Exception("[Concat]: Variable 'A' or 'B' have invalid type for concation");
        }

        return result;
    }
    public static string Textify(object val)
    {
        string result = null;

        result = val.ToString();

        return result;
    }
    //Dodělat podmínky:
    public static bool EvaluateCondition(object a, object b, string op)
    {
        bool result = false;

        switch (op)
        {
            case "==":
                result = a == b;
                break;
            case "!=":
                result = a != b;
                break;
            default:
                throw new Exception("[EvaluateCondition]: Invalid operator");
        }

        return result;
    }
}