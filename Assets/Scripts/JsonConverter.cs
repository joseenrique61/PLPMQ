using System.Collections.Generic;
using UnityEngine;

public class JsonConverter
{
    public static List<Dictionary<string, string>> Convert(string str)
    {
        List<Dictionary<string, string>> converted = new List<Dictionary<string, string>>();
        string[] splitString = str.Split('*');
        foreach (string str1 in splitString)
        {
            converted.Add(CreateDicts(str1));
        }
        return converted;
    }

    private static Dictionary<string, string> CreateDicts(string str1)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string[] splitString = str1.Split('+');
        foreach (string str2 in splitString)
        {
            char[] trimChars = {' ', '\r', '\n', '\t', '{', '}'};
            string strTrimmed = str2.Trim(trimChars);
            dict.Add(strTrimmed.Split('?')[0].Trim('\"'), strTrimmed.Split('?')[1].Trim('\"'));
        }
        return dict;
    }
}
