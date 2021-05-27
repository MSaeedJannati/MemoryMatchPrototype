using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;
    [SerializeField] GridCriterians Criterians;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GridInfo gridInfo;
    [SerializeField] Transform cardsParentTransform;
    [SerializeField] Vector2 TotalGap;
    bool hasXPadding=false;
    bool hasYPadding =false;
    #endregion
    #region MonobehaviourCallbacks
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion
    #region Functions
    [ContextMenu("create grid test")]
    void CreateGrid()
    {
        Vector3 scale = CalcScale();
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < gridInfo.rowCount; i++)
        {
            for (int j = 0; j < gridInfo.coloumnCount; j++)
            {
                if (ObjectPool.Instantiate(cardPrefab, cardsParentTransform).TryGetComponent(out Card card))
                {
                    pos = CalcPos(i, j, ref scale);
                    card.SetCardAppearence(ref pos, ref scale);
                }
            }
        }
    }
    [ContextMenu("clear the grid")]
    void clearGrid()
    {
        for (int i = cardsParentTransform.childCount - 1; i >= 0; i--)
        {
            cardsParentTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
    Vector3 CalcScale()
    {
        Vector3 scale = Vector3.one;
        float deltaY = Criterians.topLeft.position.y - Criterians.bottomRight.position.y;
        float deltaX = Criterians.bottomRight.position.x - Criterians.topLeft.position.x;
        float xSections;
        float ySections;

        if (gridInfo.coloumnCount > 1)
        {
            xSections = gridInfo.coloumnCount + TotalGap.x;
            hasXPadding = true;
        }
        else
        {
            xSections = gridInfo.coloumnCount;
            hasXPadding = false;
        }
        if (gridInfo.rowCount > 1)
        {
            ySections = gridInfo.rowCount + TotalGap.y;
            hasYPadding = true;
        }
        else
        {
            ySections = gridInfo.rowCount;
            hasYPadding = false;
        }
        scale.x = deltaX / (xSections);
        scale.y = deltaY / (ySections);
        return scale;
    }
    Vector3 CalcPos(int row, int coloumn, ref Vector3 scale)
    {
        Vector3 pos = Vector3.zero;
        float xPadding = hasXPadding ? 
            coloumn * TotalGap.x * scale.x / (gridInfo.coloumnCount - 1) : 
            0.0f;
        float yPadding = hasYPadding ?
            row * TotalGap.y * scale.y / (gridInfo.rowCount - 1) : 
            0.0f;
        pos.x = scale.x * coloumn + xPadding + scale.x / 2;
        pos.y = -(scale.y * row + yPadding + scale.y / 2);
        pos += Criterians.topLeft.position;
        return pos;

    }
    #endregion
    #region localClasses
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
    #endregion
}
