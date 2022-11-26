using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Converter
{
    private readonly string path = "";

    /// <summary>
    /// Obtiene el bloque de código en el lenguaje especificado.
    /// </summary>
    /// <param name="block">Bloque de código a obtener.</param>
    /// <param name="language">Lenguaje en el que se va a obtener el bloque de código.</param>
    /// <returns>Bloque de código en el lenguaje especificado.</returns>
    private string GetFileContent(string block, string language)
    {
        string fileContent = "";
        string file = File.ReadAllText(path);
        List<Dictionary<string, string>> jArrayFile = JsonConverter.Convert(file);
        foreach (Dictionary<string, string> jobject in jArrayFile)
        {
            string name = jobject["type"];
            if (block == name) 
            {
                fileContent = jobject[language];
                break;
            }
        }
        return fileContent;
    }

    /// <summary>
    /// Crea una nueva cadena reemplazada de sentencias con condicionales.
    /// </summary>
    /// <param name="blockType">Tipo de bloque a reemplazar.</param>
    /// <param name="cond">Condicional a reemplazar.</param>
    /// <param name="x">Código a reemplazar.</param>
    /// <param name="language">Lenguaje en el que se va a reemplazar.</param>
    /// <returns>Cadena reemplazada.</returns>
    public string WithCond(string blockType, string cond, string x, string language, int initialTab)
    {
        string plantilla = GetFileContent(blockType, language);
        string replaced = Replace(plantilla, x, "x");
        replaced = Replace(replaced, cond, "cond");
        replaced = AddTabulation(replaced, initialTab);
        return replaced;
    }

    /// <summary>
    /// Crea una nueva cadena reemplazada de sentencias de asignación.
    /// </summary>
    /// <param name="blockType">Tipo de bloque a reemplazar.</param>
    /// <param name="name">Nombre a reemplazar.</param>
    /// <param name="x">Código a reemplazar.</param>
    /// <param name="language">Lenguaje en el que se va a reemplazar.</param>
    /// <returns>Cadena reemplazada.</returns>
    public string WithName(string blockType, string name, string x, string language, int initialTab)
    {
        string plantilla = GetFileContent(blockType, language);
        string replaced = Replace(plantilla, x, "x");
        replaced = Replace(replaced, name, "name");
        replaced = AddTabulation(replaced, initialTab);
        return replaced;
    }

    /// <summary>
    /// Crea una nueva cadena reemplazada de asignaciones con tamaño de cadena.
    /// </summary>
    /// <param name="blockType">Tipo de bloque a reemplazar.</param>
    /// <param name="name">Nombre a reemplazar.</param>
    /// <param name="cant">Tamaño de la cadena a reemplazar</param>
    /// <param name="x">Código a reemplazar.</param>
    /// <param name="language">Lenguaje en el que se va a reemplazar.</param>
    /// <returns>Cadena reemplazada.</returns>
    public string WithCant(string blockType, string name,string cant, string x, string language, int initialTab)
    {
        string plantilla = GetFileContent(blockType, language);
        string replaced = Replace(plantilla, x, "x");
        replaced = Replace(replaced, name, "name");
        replaced = Replace(replaced, cant, "cant");
        replaced = AddTabulation(replaced, initialTab);
        return replaced;
    }

    /// <summary>
    /// Reemplaza el código especificado en el bloque de código.
    /// </summary>
    /// <param name="block">Bloque de código en el que se va a reemplazar.</param>
    /// <param name="toReplace">Bloque de código para reemplazar.</param>
    /// <param name="toPass">Lugar en el que se va a reemplazar.</param>
    /// <returns></returns>
    private string Replace(string block, string toReplace, string toPass)
    {
        string replaced = block.Replace(toPass, toReplace);
        return replaced;
    }

    /// <summary>
    /// Añade cierta tabulación, especificada en la plantilla.
    /// </summary>
    /// <param name="block">Código al que se le va a añadir la tabulación.</param>
    /// <returns>Cadena tabulada e indentada.</returns>
    private string AddTabulation(string block, int initialTab)
    {
        string indented = "";
        string[] blockSplitted = block.Split('~');
        foreach (string line in blockSplitted)
        {
            int num = int.Parse(line[0].ToString()) + initialTab;
            string tabString = "";
            for (int i = 0; i < num; i++)
            {
                tabString += "\t";
            }
            string newString = tabString + line.Trim(char.Parse(num.ToString()));
            indented += newString;
        }
        return indented;
    }

    public Converter()
    {
        path = Application.dataPath + @"\Plantillas\Plantillas.txt";
    }
}