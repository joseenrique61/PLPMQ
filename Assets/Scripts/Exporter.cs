using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exporter
{
    public static string language;

    public Exporter(string language)
    {
        Exporter.language = language;
    }

    public string Export()
    {
        Organizer organizer = new();
        string exported = organizer.Organize().ConvertToString();
        Debug.LogWarning(exported);
        return exported;
    }
}
