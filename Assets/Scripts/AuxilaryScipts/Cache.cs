using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainUICalsses;
using Newtonsoft.Json;

public static class Cache
{
    #region Variables
    static Level loadedLevel;
    static int lvlIndex;
    #endregion
    #region Properties
    public static Level LoadedLevel=> loadedLevel;
    public static int LvlIndex=> lvlIndex;
    #endregion
    #region Calsses
    public static void LoadLevel(Level lvl)
    {
        loadedLevel = new Level();
        int.TryParse(lvl.Name, out lvlIndex);
        //create a deep copy of lvl, maybe not neccesry yet but could be 
        //usefull when Level class grows and has fields with type of other classes
        loadedLevel.row = lvl.row;
        loadedLevel.time = lvl.time;
        loadedLevel.Cards = lvl.Cards;
        loadedLevel.coloumn = lvl.coloumn;
        loadedLevel.scoreRanges=new int[ lvl.scoreRanges.Length];
        for (int i = 0; i < loadedLevel.scoreRanges.Length; i++)
        {
            loadedLevel.scoreRanges[i] = lvl.scoreRanges[i];
        }

    }
    public static int getLastLevel()
    {
      return  PlayerPrefs.GetInt("LastLevel", 0);
    }
    public static List<int> getLevelStars()
    {
        List<int> stars = new List<int>();
        string starsRaw = PlayerPrefs.GetString("Stars", "");
        if (starsRaw.Length > 1)
            stars = JsonConvert.DeserializeObject(starsRaw, typeof(List<int>)) as List<int>;
        return stars;
    }
    public static void setLastLevel(int lvlIndex)
    {
        PlayerPrefs.SetInt("LastLevel", lvlIndex);
    }
    public static void setStars(List<int> stars)
    {
        string serilizedData = JsonConvert.SerializeObject(stars);
        PlayerPrefs.SetString("Stars", serilizedData);
    }
    #endregion
}
