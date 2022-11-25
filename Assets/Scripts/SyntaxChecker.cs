using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SyntaxChecker
{
    private Dictionary<int, IfWhileClass> openSentences = new();

    private int currentSentence = 0;

    public SyntaxChecker()
    {
        Iterate();
    }

    private void Iterate()
    {
        foreach (GameObject gameObject in FullProcessCommands.BlocksInOrder)
        {
            Properties.TypeEnum type = gameObject.GetComponent<Properties>().Type;

            if (gameObject.CompareTag("If") || gameObject.CompareTag("While"))
            {
                AddOpenS(gameObject);
            }
            else if (gameObject.CompareTag("Else"))
            {
                try
                {
                    //SetCurrentS(type);
                    if (!(type == openSentences[currentSentence].ifWhileGameObject.GetComponent<Properties>().Type))
                    {
                        Debug.LogError("Se inició una sentencia Else sin un If.");
                        return;
                    }

                    if (!openSentences[currentSentence].hasElse)
                    {
                        openSentences[currentSentence].hasElse = true;
                    }
                    else
                    {
                        Debug.LogError("Hay una sentencia Else luego de otro Else.");
                        return;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError("Hay una sentencia Else luego del cierre del If.");
                    return;
                }
            }
            else if (gameObject.CompareTag("ElseIf"))
            {
                try
                {
                    //SetCurrentS(type);
                    if (!(type == openSentences[currentSentence].ifWhileGameObject.GetComponent<Properties>().Type))
                    {
                        Debug.LogError("Se inició una sentencia ElseIf sin un If.");
                        return;
                    }

                    if (openSentences[currentSentence].hasElse)
                    {
                        Debug.LogError("Hay una sentencia ElseIf luego de un Else.");
                        return;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError("Hay una sentencia ElseIf luego del cierre del If.");
                    return;
                }
            }
            else if (gameObject.CompareTag("EndIf") || gameObject.CompareTag("EndWhile"))
            {
                try
                {
                    //SetCurrentS(type);
                    if (type == openSentences[currentSentence].ifWhileGameObject.GetComponent<Properties>().Type)
                    {
                        RemoveOpenS();
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError("No es correcta la sintaxis.");
                    return;
                }
            }
        }

        if (openSentences.Count > 0)
        {
            Debug.LogError($"Hay {openSentences.Count} sentencia/s abierta/s todavía.");
        }

        if (!FullProcessCommands.BlocksInOrder[^1].CompareTag("Fin"))
        {
            Debug.LogError("No hay un bloque de fin.");
        }
    }

    private void AddOpenS(GameObject gameObject)
    {
        currentSentence++;
        openSentences.Add(currentSentence, new IfWhileClass(gameObject));
    }

    private void RemoveOpenS()
    {
        openSentences.Remove(currentSentence);
        currentSentence--;
    }

    //private void SetCurrentS(Properties.TypeEnum type)
    //{
    //    for (int i = currentSentence; i >= openSentences.Count; i--)
    //    {
    //        if (type == openSentences[i].ifWhileGameObject.GetComponent<Properties>().Type)
    //        {
    //            Debug.LogWarning(i != currentSentence ? "Changed i." : "Stays the same.");
    //            currentSentence = i;
    //        }
    //    }
    //}
}
