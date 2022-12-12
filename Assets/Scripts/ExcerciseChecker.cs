using TMPro;
using UnityEngine;

public class ExcerciseChecker : MonoBehaviour
{
    [HideInInspector]
    public bool selected;

    public GameObject initialParent;

    public GameObject exportParent;

    public TextMeshProUGUI headerTMP;
    
    public TextMeshProUGUI bodyTMP;

    public void Selected()
    {
        selected = true;
    }

    public void Unselected()
    {
        selected = false;
    }

    public void Update()
    {
        if (selected && OVRInput.GetUp(OVRInput.RawButton.A))
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
                headerTMP.text = "�Correcto!";
                bodyTMP.text = "";
                exportParent.SetActive(true);
                initialParent.SetActive(false);
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
