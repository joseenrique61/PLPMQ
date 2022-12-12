using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SubBlockClass : ScriptableObject
{
    private List<Object> blocks = new();
    private string language;
    public static int currentTab = 0;

    public void Init(Object[] blocks)
    {
        language = Exporter.currentLanguage;
        this.blocks = blocks.ToList();
        this.blocks.RemoveAt(this.blocks.Count - 1);

        if (this.blocks[0] is GameObject firstGameObject)
        {
            this.blocks.Remove(firstGameObject);
            this.blocks.Add(firstGameObject);
        }
    }

    public string ConvertToString()
    {
        StringBuilder fullString = new();
        Converter converter = new();
        bool resetFullString = false;
        currentTab++;
        foreach (Object block in blocks)
        {
            string gameObjectString = "";
            if (block is GameObject gameObject)
            {
                switch (gameObject.tag)
                {
                    case "If" or "ElseIf" or "While":
                        currentTab--;
                        gameObjectString = converter.WithCond(gameObject.tag, gameObject.GetComponent<Properties>().Condition, fullString.ToString(), language, currentTab);
                        currentTab++;
                        fullString.Clear();
                        resetFullString = true;
                        break;
                    case "Else":
                        currentTab++;
                        gameObjectString = converter.WithInstruction("Else", fullString.ToString(), language, currentTab);
                        currentTab--;
                        fullString.Clear();
                        resetFullString = true;
                        break;
                    case "Input" or "Output" or "Proceso":
                        gameObjectString = converter.WithInstruction(gameObject.tag, gameObject.GetComponent<Properties>().Instruction, language, currentTab);
                        break;
                    case "Int" or "Float" or "String" or "Bool":
                        gameObjectString = converter.WithName(gameObject.tag, gameObject.GetComponent<Properties>().Name, language, currentTab);
                        break;
                }
            }
            else if (block is SubBlockClass subBlock)
            {
                gameObjectString = subBlock.ConvertToString();
            }

            if (resetFullString)
            {
                fullString.Append(gameObjectString);
            }
            else
            {
                fullString.AppendLine(gameObjectString);
            }
        }
        currentTab--;
        return fullString.ToString();
    }
}
