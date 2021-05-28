using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainUICalsses;
using Newtonsoft.Json;
using GameManagerCalsses;

public static class Cache
{
    #region Variables
    static Level loadedLevel;
    static int lvlIndex;
    static bool isEditor;
    #endregion
    #region Properties
    public static Level LoadedLevel=> loadedLevel;
    public static int LvlIndex=> lvlIndex;
    public static bool IsEditor
    {
        get 
        {
            return isEditor; 
        }
        set 
        {
            isEditor = value;
        }
    }
    #endregion
    #region Calsses
    public static void LoadLevelJson(int levelIndex, string json, bool isEditor)
    {
        loadedLevel = new Level();
        lvlIndex = levelIndex;
        IsEditor = isEditor;
        Hashtable levelData = new Hashtable();
        levelData = JsonConvert.DeserializeObject(json, typeof(Hashtable)) as Hashtable;
        if (levelData.ContainsKey("level_time"))
        {
            int.TryParse(levelData["level_time"].ToString(), out loadedLevel.time);
        }
        loadedLevel.gridInfo = new GridInfo();
        if (levelData.ContainsKey("grid_Info"))
        {
            loadedLevel.gridInfo = JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(levelData["grid_Info"]),
                typeof(GridInfo)
                ) as GridInfo;
        }
        if (levelData.ContainsKey("score_ranges"))
        {
            loadedLevel.scoreRanges = JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(levelData["score_ranges"]),
                typeof(int[])
                ) as int[];
        }
        if (levelData.ContainsKey("grid_status"))
        {
            loadedLevel.gridStatus = JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(levelData["grid_status"]),
                typeof(int[,])
                ) as int[,];
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
