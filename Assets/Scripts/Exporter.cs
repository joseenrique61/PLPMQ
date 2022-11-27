using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Exporter : ScriptableObject
{
    public static string language;

    [SerializeField]
    public TextMeshPro text;

    public string Export()
    {
        Organizer organizer = new();
        string exported = organizer.Organize().ConvertToString();
        Debug.LogWarning(exported);
        TextMeshPro tmp = Instantiate(text, Vector3.zero, Quaternion.identity);
        tmp.text = exported;
        return exported;
    }

    public void Init(string language, TextMeshPro text)
    {
        Exporter.language = language;
        this.text = text;
    }

    private string TMPString(string exportedText)
    {
        StringBuilder stringBuilder = new();
        string[] dividedString = exportedText.Split('\n');
        foreach (string str in dividedString)
        {
            string str1 = str.Trim('r');
            stringBuilder.AppendLine(str1);
        }
        return stringBuilder.ToString();
    }
}
