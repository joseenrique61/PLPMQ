using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Exporter : MonoBehaviour
{
    public static string currentLanguage;

    public string thisBlockLanguage;

    public TextMeshProUGUI headerTMP;

    public TextMeshProUGUI bodyTMP;

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
        currentLanguage = thisBlockLanguage;
        Export();
    }

    public void Export()
    {
        Organizer organizer = new();
        string exported = organizer.Organize().ConvertToString();
        headerTMP.text = $"Código exportado a {currentLanguage.FirstCharacterToUpper()}";
        bodyTMP.text = exported;
    }
}
