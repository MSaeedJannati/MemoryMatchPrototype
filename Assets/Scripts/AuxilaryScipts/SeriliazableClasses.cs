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