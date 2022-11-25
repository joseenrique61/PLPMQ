using System.Collections.Generic;
using UnityEngine;

public class SyntaxChecker
{
    private Dictionary<int, GameObject> openSentences = new();

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
                currentSentence++;
                openSentences.Add(currentSentence, gameObject);
            }
            else if (gameObject.CompareTag("EndIf") || gameObject.CompareTag("EndWhile"))
            {
                try
                {
                    if (type == openSentences[currentSentence].GetComponent<Properties>().Type)
                    {
                        openSentences.Remove(currentSentence);
                        currentSentence--;
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

    private struct IfStruct
    {
        public bool hasElse;
    }
}
