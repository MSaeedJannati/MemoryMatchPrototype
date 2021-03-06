using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region GameManager Classes
namespace GameManagerCalsses
{
    [System.Serializable]
    public class GridCriterians
    {
        public Transform topLeft;
        public Transform bottomRight;
    }
    [System.Serializable]
    public class GridInfo
    {
        public int rowCount;
        public int coloumnCount;
    }
}
#endregion

#region MainUIClasses
namespace MainUICalsses
{
    [System.Serializable]
    public class Level
    {
        public string Name;
        public int time;
        public int Cards;
        public GameManagerCalsses.GridInfo gridInfo;
        public int[] scoreRanges = new int[3];
        public int[,] gridStatus;
    }
}
#endregion
#region enums
namespace CustomEnums
{
    [System.Serializable]
    public enum Scenes
    {
        MAIN,
        GAME,
        EDITOR
    }
}
#endregion