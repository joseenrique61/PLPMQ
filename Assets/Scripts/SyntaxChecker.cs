using System.Collections.Generic;
using UnityEngine;

public class SyntaxChecker
{
    private Dictionary<int, IfWhileClass> openSentences = new();

    private int currentSentence = 0;

    /// <summary>
    /// Itera a través de la lista con los bloques en orden y determina si hay errores de sintaxis o no.
    /// </summary>
    /// <returns>True si no hay errores, false si hay.</returns>
    public bool CheckSyntax()
    {
        bool correctSyntax = true;

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
                    if (!(type == openSentences[currentSentence].ifWhileGameObject.GetComponent<Properties>().Type))
                    {
                        Debug.LogError("Se inició una sentencia Else sin un If.");
                        correctSyntax = false;
                        break;
                    }

                    if (!openSentences[currentSentence].hasElse)
                    {
                        openSentences[currentSentence].hasElse = true;
                    }
                    else
                    {
                        Debug.LogError("Hay una sentencia Else luego de otro Else.");
                        correctSyntax= false;
                        break;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError("Hay una sentencia Else luego del cierre del If.");
                    correctSyntax = false;
                    break;
                }
            }
            else if (gameObject.CompareTag("ElseIf"))
            {
                try
                {
                    if (!(type == openSentences[currentSentence].ifWhileGameObject.GetComponent<Properties>().Type))
                    {
                        Debug.LogError("Se inició una sentencia ElseIf sin un If.");
                        correctSyntax = false;
                        break;
                    }

                    if (openSentences[currentSentence].hasElse)
                    {
                        Debug.LogError("Hay una sentencia ElseIf luego de un Else.");
                        correctSyntax = false;
                        break;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError("Hay una sentencia ElseIf luego del cierre del If.");
                    correctSyntax = false;
                    break;
                }
            }
            else if (gameObject.CompareTag("EndIf") || gameObject.CompareTag("EndWhile"))
            {
                try
                {
                    if (type == openSentences[currentSentence].ifWhileGameObject.GetComponent<Properties>().Type)
                    {
                        RemoveOpenS();
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogError("No es correcta la sintaxis.");
                    correctSyntax = false;
                    break;
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

        return correctSyntax;
    }

    /// <summary>
    /// Añade un bloque a la lista de openSentences.
    /// </summary>
    /// <param name="gameObject">Objeto a añadir.</param>
    private void AddOpenS(GameObject gameObject)
    {
        currentSentence++;
        openSentences.Add(currentSentence, new IfWhileClass(gameObject));
    }
    
    /// <summary>
    /// Remueve el último bloque de openSentences.
    /// </summary>
    private void RemoveOpenS()
    {
        openSentences.Remove(currentSentence);
        currentSentence--;
    }
}
