using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SubBlockClass : Object
{
    private List<Object> blocks = new();
    private readonly string language;
    public int currentTab = 0;

    public SubBlockClass(Object[] blocks)
    {
        language = Exporter.language;
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
        currentTab++;
        foreach (Object block in blocks)
        {
            string gameObjectString = "";
            if (block is GameObject gameObject)
            {
                switch (gameObject.tag)
                {
                    case "If" or "ElseIf" or "While":
                        gameObjectString = converter.WithCond(gameObject.tag, gameObject.GetComponent<Properties>().Condition, fullString.ToString(), language, currentTab);
                        fullString = new StringBuilder();
                        break;
                    case "Else":
                        gameObjectString = converter.WithInstruction("Else", fullString.ToString(), language, currentTab);
                        fullString = new StringBuilder();
                        break;
                    case "Proceso":
                        gameObjectString = gameObject.GetComponent<Properties>().Instruction;
                        break;
                    case "Input" or "Output" or "Int" or "Float" or "String" or "Bool":
                        gameObjectString = converter.WithInstruction(gameObject.tag, gameObject.GetComponent<Properties>().Instruction, language, currentTab);
                        break;

                }
            }
            else if (block is SubBlockClass subBlock)
            {
                gameObjectString = subBlock.ConvertToString();
            }
            fullString.AppendLine(gameObjectString);
        }
        currentTab--;
        return fullString.ToString();
    }
}
