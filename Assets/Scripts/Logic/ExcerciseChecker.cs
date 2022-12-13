using TMPro;
using UnityEngine;

public class ExcerciseChecker : MonoBehaviour
{
    [HideInInspector]
    public bool hovered;

    public GameObject initialParent;

    public GameObject exportParent;

    public GameObject nextLevel;

    public TextMeshProUGUI headerTMP;
    
    public TextMeshProUGUI bodyTMP;

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
            CheckExcercise();
        }
    }

    public void CheckExcercise()
    {
        SyntaxChecker syntaxChecker = new();
        if (syntaxChecker.CheckSyntax(out string error))
        {
            if (ListCommands.CheckList())
            {
                headerTMP.text = "¡Correcto!";
                bodyTMP.text = "";
                exportParent.SetActive(true);
                //initialParent.SetActive(false);
                nextLevel.SetActive(true);
            }
            else
            {
                headerTMP.text = "Incorrecto";
            }
        }
        else
        {
            headerTMP.text = "Error de sintaxis";
            bodyTMP.text = error;
        }
    }
}
