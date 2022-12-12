using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ListCommands
{
    private static List<int> ids = new();
    public static void SaveList()
    {
        string path = $@"{Application.dataPath}\Lists\ListSave{SceneManager.GetActiveScene().name}.dat";

        ids = GetIDs();

        FileStream fs = new(path, FileMode.Create);
        BinaryFormatter bf = new();
        bf.Serialize(fs, ids);

        fs.Close();
    }

    public static bool CheckList()
    {
        string path = $@"{Application.dataPath}\Lists\ListSave{SceneManager.GetActiveScene().name}.dat";

        FileStream stream = new(path, FileMode.Open);
        BinaryFormatter bformatter = new();

        object bf = bformatter.Deserialize(stream);
        List<int> correctIds = (List<int>)bf;
        stream.Close();

        List<int> currentIds = GetIDs();

        bool result = CompareLists(currentIds, correctIds);
        return result;
    }

    private static bool CompareLists(List<int> list1, List<int> list2)
    {
        if (list1.Count == list2.Count)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private static List<int> GetIDs()
    {
        List<int> ids = new();
        foreach (GameObject block in FullProcessCommands.BlocksInOrder)
        {
            ids.Add(block.GetComponent<BlockID>().id);
        }
        return ids;
    }
}
