using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorLevelCard : MonoBehaviour
{
    #region Variables
    [SerializeField] TMP_Text indexTxt;
    [SerializeField] int index;
    #endregion
    #region Properties
    public int Index => index;
    #endregion
    #region Functions
    public void OnClick()
    {
        EditorManager.instance.levelIndex = index;
        EditorManager.instance.LoadLevel(index);
    }
    public void SetIndex(int indx)
    {
        index = indx;
        indexTxt.text = (indx+1).ToString();
    }
    #endregion
}
