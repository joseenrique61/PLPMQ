using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Exporter : MonoBehaviour
{
    public static string language;

    public string _language;

    public TextMeshProUGUI text;

    [HideInInspector]
    public bool hovered = false;

    public void Hovered()
    {
        hovered = true;
    }

    public void Unhovered()
    {
        hovered = false;
    }

    public void Update()
    {
        if (hovered && OVRInput.GetUp(OVRInput.RawButton.A))
        {
            Clicked();
        }
    }

    public void Clicked()
    {
        language = _language;
        Export();
    }

    public void Export()
    {
        SyntaxChecker syntaxChecker = new();
        if (syntaxChecker.CheckSyntax(out string error))
        {
            Organizer organizer = new();
            string exported = organizer.Organize().ConvertToString();
            text.text = exported;
        }
        else
        {
            text.text = error;
        }
    }
}
