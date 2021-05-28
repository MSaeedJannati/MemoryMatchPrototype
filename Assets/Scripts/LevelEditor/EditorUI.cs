using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorUI : MonoBehaviour
{
    #region Variables
    [SerializeField] TMP_InputField[] scoreInpts;
    [SerializeField] TMP_InputField[] gridInpts;
    [SerializeField] TMP_InputField timerInpt;
    [SerializeField] TMP_Text lvlTxt;
    [SerializeField] TMP_Text cardsTxt;
    #endregion
    #region Monobehaviour callBacks
    private void OnEnable()
    {
        EditorManager.cardActivisionChange += SetCardsText;
    }
    private void OnDisable()
    {
        EditorManager.cardActivisionChange -= SetCardsText;
    }
    #endregion
    #region Functions
    public int[] getScoreRanges()
    {
        int[] scores = new int[3];
        for (int i = 0; i < scores.Length; i++)
        {
            int.TryParse(scoreInpts[i].text, out scores[i]);
        }
        return scores;
    }
    public void SetScoreRanges(int[] scores)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scoreInpts[i].text = scores[i].ToString();
        }
    }
    public int getTime()
    {
        int time = 0;
        int.TryParse(timerInpt.text, out time);
        return time;
    }
    public void SetTime(int value)
    {
        timerInpt.text = value.ToString();
    }
    public int[] getGridInfo()
    {
        int[] gridDimensions = new int[2];
        for (int i = 0; i < gridDimensions.Length; i++)
        {
            int.TryParse(gridInpts[i].text, out gridDimensions[i]);
        }
        return gridDimensions;
    }
    public void SetGridInfo(int[] gridInfo)
    {
        for (int i = 0; i < gridInfo.Length; i++)
        {
            gridInpts[i].text = gridInfo[i].ToString();
        }
    }
    public void SetLevelTxt(int value)
    {
        lvlTxt.text = value.ToString();
    }
    public void SetCardsText(int value)
    {
        cardsTxt.text = value.ToString();
    }
    public void BackClicked()
    {
        EditorManager.instance.BackToSelection();
    }
    public void SaveClicked()
    {
        EditorManager.instance.Save();
    }
    public void TestClicked()
    {
        EditorManager.instance.playTestLevel();
    }
    public void GenerateClicked()
    {
        EditorManager.instance.OnGenerate(getGridInfo(), getScoreRanges(), getTime());
    }

    #endregion
}
