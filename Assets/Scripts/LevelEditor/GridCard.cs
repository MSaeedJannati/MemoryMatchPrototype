using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCard : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform mTransform;
    [SerializeField] SpriteRenderer Image;
    [SerializeField] bool isActive;
    //0==>acive, 1====>Disactive
    [SerializeField] Color[] Colours;

    int row;
    int coloumn;
    #endregion
    #region Monobehaviour callbacks
    private void OnEnable()
    {
        //activate(true);
    }
    #endregion
    #region Functions
    public void OnClicked()
    {
        activate(!isActive);
        int value = isActive ? 1 : 0;
        EditorManager.instance.ChangeGridUnitValue(row, coloumn, value);
    }
    public void activate(bool activate)
    {
        isActive = activate;
        Image.color = activate ? Colours[0] : Colours[1];
        int delta = activate ? 1 : -1;
        EditorManager.instance.CardActivisionChanged(delta);
    }
    public void SetCardAppearence(ref Vector3 pos, ref Vector3 scale)
    {
        mTransform.position = pos;
        mTransform.localScale = scale;
    }
    public void SetRowAnCol(int Row, int Col)
    {
        row = Row;
        coloumn = Col;
    }
    #endregion
}
