using System;

namespace ObjectIO;
public class Interpreter
{
    public string? ID { get; set; }
    public Dictionary<string, object> Variables { get; set; }

    public Interpreter()
    {
        ID = Guid.NewGuid().ToString();
        Variables = new Dictionary<string, object>();
    }

    public void Execute(string src)
    {
        string[] lines = src.Split('\n');
        int indexLine = 0;
        foreach (string line in lines)
        {
            string[] words = line.Split(' ');
            if (line.StartsWith("MAKE"))
            {
                string varName = line.Remove(0, 5);
                Variables.Add(varName, null);
            }
            if (line.StartsWith("SET"))
            {
                string locName = words[1].Remove(0,1);
                string value = line.Remove(0, 5 + locName.Length).Trim();
                if (Variables.ContainsKey(locName) == false)
                {
                    throw new Exception("[Execution:(line:" + (indexLine + 1) + ")]: Variable does not exist!");
                }
                Variables[locName] = Parser.ParseValue(value, Variables);
            }
            if (line.StartsWith("PRINT"))
            {
                string str = line.Remove(0, 6);
                Console.WriteLine(Parser.ParseValue(str, Variables));
            }
            indexLine++;
        }
    }
}