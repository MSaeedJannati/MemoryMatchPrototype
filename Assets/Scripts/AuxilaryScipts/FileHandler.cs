using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class FileHandler 
{
    #region Variables
  
    #endregion
    #region Functions
    public static bool FileExsits(string fullAddress)
    {
        return File.Exists(fullAddress);
    }
    public static void SaveFile(string json, string fullAddress)
    {
        if (FileExsits(fullAddress))
        {
            File.Delete(fullAddress);
        }
        FileStream stream = new FileStream(fullAddress, FileMode.Create);
        using (StreamWriter writer=new StreamWriter(stream))
        {
            writer.Write(json);
        }
    }
    public static string LoadFile(string fullAddress)
    {
        using (StreamReader reader = new StreamReader(fullAddress))
        {
            return reader.ReadToEnd();
        }
    }

    #endregion
}
