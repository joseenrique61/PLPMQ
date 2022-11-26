using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Organizer
{
    private Dictionary<int, Object> dividedBlocks = new();

    private int currentIndexOfDividedBlocks = 0;

    private Dictionary<int, IfWhileClass> openSentences = new();

    private int currentSentence = 0;

    private List<Object> internalBlocksInOrder = new();

    public Organizer() 
    {
        Object[] temporalArray = FullProcessCommands.BlocksInOrder.ToArray();
        internalBlocksInOrder = temporalArray.ToList();
    }

    /// <summary>
    /// Organiza los bloques.
    /// </summary>
    /// <returns>SubBlockClass con los bloques organizados.</returns>
    public SubBlockClass Organize()
    {
        DivideBlocks();
        
        return internalBlocksInOrder[0] is SubBlockClass gameObject? gameObject : null;
    }

    /// <summary>
    /// Divide los bloques.
    /// </summary>
    private void DivideBlocks()
    {
        OrganizeIfBlocks();

        while (true)
        {
            GameObject lastGameObject = null;
            bool addBlock = false;

            foreach (GameObject gameObject in internalBlocksInOrder.Cast<GameObject>())
            {
                Properties.TypeEnum type = gameObject.GetComponent<Properties>().Type;

                if (gameObject.CompareTag("If") || gameObject.CompareTag("While"))
                {
                    AddOpenS(gameObject);
                }
                else if (gameObject.CompareTag("EndIf") || gameObject.CompareTag("EndElseIf") || gameObject.CompareTag("EndElse") || gameObject.CompareTag("EndWhile"))
                {
                    if (type == openSentences[currentSentence].ifWhileGameObject.GetComponent<Properties>().Type)
                    {
                        lastGameObject= gameObject;
                        addBlock = true;
                        break;
                    }
                }
            }
            
            if (addBlock)
            {
                AddDividedBlock(GetInitialBlock(lastGameObject), lastGameObject);
            }
            else
            {
                break;
            }
        }

        if (internalBlocksInOrder[0] is GameObject initialGameObject && internalBlocksInOrder[^1] is GameObject finalGameObject)
        {
            AddDividedBlock(initialGameObject, finalGameObject);
        }
    }

    /// <summary>
    /// Organiza los bloques If.
    /// </summary>
    private void OrganizeIfBlocks()
    {
        Dictionary<string, string> tags = new()
        {
            { "If", "EndIf" },
            { "ElseIf", "EndElseIf" },
            { "Else", "EndElse" },
            { "While", "EndWhile" }
        };

        foreach (GameObject gameObject in FullProcessCommands.BlocksInOrder)
        {
            if (tags.Keys.Contains(gameObject.tag))
            {
                GameObject newGameObject = new()
                {
                    tag = tags[gameObject.tag]
                };

                internalBlocksInOrder.Insert(internalBlocksInOrder.IndexOf(gameObject) - 1, newGameObject);
            }

            if (gameObject.CompareTag("EndIf"))
            {
                internalBlocksInOrder.Remove(gameObject);
            }
        }
    }

    /// <summary>
    /// Obtiene el bloque inicial de la sentencia actual.
    /// </summary>
    /// <param name="currentBlock">Bloque actual, con el cual se va a obtener el bloque inicial.</param>
    /// <returns>Bloque inicial de la sentencia actual.</returns>
    private GameObject GetInitialBlock(GameObject currentBlock)
    {
        GameObject gameObject = new();
        string initialTag = "";

        switch (currentBlock.tag)
        {
            case "EndIf":
                initialTag = "If";
                break;
            case "EndWhile":
                initialTag = "While";
                break;
            case "EndElseIf":
                initialTag = "ElseIf";
                break;
            case "EndElse":
                initialTag = "Else";
                break;
        }

        for (int i = internalBlocksInOrder.IndexOf(currentBlock); i >= internalBlocksInOrder.Count; i--)
        {
            if (internalBlocksInOrder[i] is GameObject currentGameObject)
            {
                if (currentGameObject.CompareTag(initialTag))
                {
                    gameObject = currentGameObject;
                    break;
                }
            }
        }
        return gameObject;
    }

    /// <summary>
    /// Añade el sub-bloque divido a dividedBlocks.
    /// </summary>
    /// <param name="initialBlock">Bloque inicial.</param>
    /// <param name="finalBlock">Bloque final.</param>
    private void AddDividedBlock(GameObject initialBlock, GameObject finalBlock)
    {
        RemoveOpenS();
        List<GameObject> blocks = new();
        for (int i = internalBlocksInOrder.IndexOf(initialBlock); i <= internalBlocksInOrder.IndexOf(finalBlock); i++)
        {
            if (internalBlocksInOrder[i] is GameObject currentGameObject)
            blocks.Add(currentGameObject);
        }
        currentIndexOfDividedBlocks++;
        SubBlockClass subBlock = new(blocks.ToArray());
        dividedBlocks.Add(currentIndexOfDividedBlocks, subBlock);
        ReplaceInternalBIO(blocks, subBlock);
    }

    /// <summary>
    /// Reemplaza los bloques por un SubBlockClass que los contiene.
    /// </summary>
    /// <param name="gameObjects">Lista con los bloques.</param>
    /// <param name="subBlock">SubBlockClass en el que están contenidos los bloques a reemplazar.</param>
    private void ReplaceInternalBIO(List<GameObject> gameObjects, SubBlockClass subBlock)
    {
        internalBlocksInOrder.Insert(internalBlocksInOrder.IndexOf(gameObjects[0]), subBlock);

        foreach (GameObject gameObject in gameObjects)
        {
            internalBlocksInOrder.Remove(gameObject);
        }
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
